using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizPlatform.DTO;
using QuizPlatform.DTO.Auth;
using QuizPlatform.Helder;
using QuizPlatform.Infrastructure;
using QuizPlatform.Models;
using QuizPlatform.Repository;
using QuizPlatform.Response;
using QuizPlatform.Validtion.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using QuizPlatform.Enums;


namespace QuizPlatform.BLL
{
    public class AuthBLL : IAuthBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AuthSetting _authSetting;

        private readonly IPasswordHasher _passwordHasher;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public AuthBLL(IUnitOfWork UnitOfWork, IMapper mapper, IOptions<AuthSetting> authSetting, IPasswordHasher PasswordHasher, IRepository<User> UserRepository, IRepository<RefreshToken> refreshTokenRepository)
        {
            _unitOfWork = UnitOfWork;
            _mapper = mapper;
            _authSetting = authSetting.Value;
            _passwordHasher = PasswordHasher;
            _userRepository = UserRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<IResponse<bool>> RegisterAsync(RegisterDto registerDto)
        {
            var response = new Response<bool>();
            try
            {
                var validation = await new RegisterDtoValidator().ValidateAsync(registerDto);
                if (!validation.IsValid)
                {
                     return response.CreateResponse(validation.Errors);
                }

                if (await _userRepository.AnyAsync(e => e.Email.ToLower().Equals(registerDto.Email.ToLower()) ))
                    return response.CreateResponse(MessageCodes.AlreadyExists);

                await _userRepository.AddAsync(_mapper.Map<User>(registerDto));

             await   _unitOfWork.CommitAsync();
                return response.CreateResponse(true);
            } 
            catch (Exception e) { 

            
                return response.CreateResponse(e);

            }


        }



        public async Task<IResponse<TokenResultDto>> LoginAsync(LoginDto loginDto)
        {
            var response = new Response<TokenResultDto>();
            try
            {
                var validation = await new LoginDtoValidator().ValidateAsync(loginDto);

                if (!validation.IsValid)
                {
                    response.CreateResponse(validation.Errors);
                    return response;
                }

                var user = await _userRepository.GetAsync(e => e.Email.ToLower().Equals(loginDto.Email.ToLower()));


                if (user is null)
                {
                    response.CreateResponse(MessageCodes.InvalidLoginCredentials);
                    return response;
                }


                if (!_passwordHasher.VerifyHashedPassword(loginDto.Password, user.Password))
                {
                    response.CreateResponse(MessageCodes.InvalidLoginCredentials);
                    return response;
                }

                var generatedJwtToken = await GenerateJwtTokenAsync(user);

                var generatedRefreshToken = await GenerateRefreshTokenAsync(generatedJwtToken.Jti, user.Id);

                var tokenResultDto = new TokenResultDto
                {
                    Token = generatedJwtToken.Token,
                    RefreshToken = generatedRefreshToken
                };

                response.CreateResponse(tokenResultDto);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }


        public async Task<IResponse<TokenResultDto>> RefreshTokenAsync(RefreshTokenDto refreshTokenDto, TokenValidationParameters tokenValidationParameters)
        {
            var response = new Response<TokenResultDto>();

            try
            {
                var validation = await new RefreshTokenDtoValidator().ValidateAsync(refreshTokenDto);

                if (!validation.IsValid)
                {
                    response.CreateResponse(validation.Errors);
                    return response;
                }

                var verifyTokenResult = await VerifyTokenAsync(refreshTokenDto, tokenValidationParameters);

                if (!verifyTokenResult.IsSuccess)
                {
                    response.AppendErrors(verifyTokenResult.Errors)
                            .CreateResponse();

                    return response;
                }

                var storedToken = verifyTokenResult.Data;

                // generate new tokens.
                var user = await _userRepository.GetAsync(e => e.Id == storedToken.UserId );

                if (user is null)
                {
                    response.CreateResponse(MessageCodes.NotFound);
                    return response;
                }

                var newJwtToken = await GenerateJwtTokenAsync(user);

                var newRefreshToken = await UpdateRefreshTokenAsync(storedToken, newJwtToken.Jti);

                var tokenResultDto = new TokenResultDto
                {
                    Token = newJwtToken.Token,
                    RefreshToken = newRefreshToken
                };

                response.CreateResponse(tokenResultDto);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }

        public async Task<IResponse<bool>> LogoutAsync(LogoutDto logoutDto)
        {
            var response = new Response<bool>();

            try
            {
                var validation = await new LogoutDtoValidator().ValidateAsync(logoutDto);

                if (!validation.IsValid)
                {
                    response.CreateResponse(validation.Errors);
                    return response;
                }

                var existRefreshToken = await _refreshTokenRepository.GetAsync(rt => rt.Token == logoutDto.RefreshToken);

                if (existRefreshToken is null)
                {
                    response.CreateResponse(MessageCodes.NotFound);
                    return response;
                }

                _refreshTokenRepository.Delete(existRefreshToken);
                await _unitOfWork.CommitAsync();

                response.CreateResponse(true);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }

        #region helder
        private async Task<JwtTokenDto> GenerateJwtTokenAsync(User user)
        {
            return await Task.Run(() =>
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_authSetting.Jwt.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _authSetting.Jwt.Issuer,
                    Subject = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(TokenClaimTypeEnum.Id.ToString(), user.Id.ToString()),
                        new Claim(TokenClaimTypeEnum.Email.ToString(), user.Email),
                        new Claim(TokenClaimTypeEnum.Name.ToString(), user.Name),

                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    }),
                    Expires = DateTime.UtcNow.Add(_authSetting.Jwt.TokenExpiryTime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

               
                var token = jwtTokenHandler.CreateToken(tokenDescriptor);

                var jwtToken = jwtTokenHandler.WriteToken(token);

                return new JwtTokenDto
                {
                    Jti = token.Id,
                    Token = jwtToken,
                };
            });
        }
        private async Task<string> GenerateRefreshTokenAsync(string jti, int Id)
        {
            var refreshToken = new RefreshToken
            {
                Jti = jti,
                UserId = Id,
                ExpireDate = DateTime.UtcNow.AddMonths(_authSetting.Jwt.RefreshToken.RefreshTokenExpiryInMonths),
                CreateDate = DateTime.UtcNow,
                Token = $"{GenerateRandom(_authSetting.Jwt.RefreshToken.TokenLength)}{Guid.NewGuid()}"
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.CommitAsync();

            return refreshToken.Token;
        }
        private string GenerateRandom(int length)
        {
            var random = new Random();

            return new string(Enumerable.Repeat("0123456789ABCDEFGHIJ0123456789KLMNOPQRST0123456789UVWXYZ012345", length)
                                        .Select(s => s[random.Next(s.Length)])
                                        .ToArray());
        }
        private async Task<IResponse<RefreshToken>> VerifyTokenAsync(RefreshTokenDto refreshTokenDto, TokenValidationParameters tokenValidationParameters)
        {
            var response = new Response<RefreshToken>();

            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();

                // prevent validate token lifetime.
                tokenValidationParameters.ValidateLifetime = false;

                // 01- Validate token is a propper jwt token formatting.
                var claimsPrincipal = jwtTokenHandler.ValidateToken(refreshTokenDto.Token, tokenValidationParameters, out var validatedToken);

                // 02- Validate token has been encrypted using the encryption that we've specified. 
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var isSameEncryption = jwtSecurityToken.Header
                                                           .Alg
                                                           .Equals(SecurityAlgorithms.HmacSha256,
                                                                   StringComparison.InvariantCultureIgnoreCase);

                    if (!isSameEncryption)
                    {
                        response.CreateResponse(MessageCodes.InvalidToken);

                        tokenValidationParameters.ValidateLifetime = true;
                        return response;
                    }
                }


                var utcLongExpiryDate = long.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcLongExpiryDate);

                
                var storedToken = await _refreshTokenRepository.GetAsync(rt => rt.Token == refreshTokenDto.RefreshToken);

                if (storedToken is null)
                {
                    response.CreateResponse(MessageCodes.NotFound);

                    // reset lifetime to valdiate it.
                    tokenValidationParameters.ValidateLifetime = true;
                    return response;
                }

                // 07- Validate jwt token Jti matches refresh token jti in database.
                var jti = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

                if (!storedToken.Jti.Equals(jti))
                {
                    response.CreateResponse(MessageCodes.TokensDoNotMatch);

                    // reset lifetime to valdiate it.
                    tokenValidationParameters.ValidateLifetime = true;
                    return response;
                }

                response.CreateResponse(storedToken);
            }
            catch (Exception ex)
            {
                response.CreateResponse(MessageCodes.InvalidToken);
            }

            // reset lifetime to valdiate it.
            tokenValidationParameters.ValidateLifetime = true;
            return response;
        }
        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // utc time is an integer (long) number of seconds from the 1970/1/1 till now. 
            var datetimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return datetimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
        }
        private async Task<string> UpdateRefreshTokenAsync(RefreshToken refreshToken, string jti)
        {
            refreshToken.Jti = jti;
            refreshToken.Token = $"{GenerateRandom(_authSetting.Jwt.RefreshToken.TokenLength)}{Guid.NewGuid()}";

            _refreshTokenRepository.Update(refreshToken);
            await _unitOfWork.CommitAsync();

            return refreshToken.Token;
        }
        #endregion
    }
}
