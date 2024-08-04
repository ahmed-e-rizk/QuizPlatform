using FluentValidation;
using QuizPlatform.DTO.Auth;
using QuizPlatform.Enums;
using QuizPlatform.Helder;

namespace QuizPlatform.Validtion.Auth
{
    public class RefreshTokenDtoValidator : DtoValidationAbstractBase<RefreshTokenDto>
    {
        public RefreshTokenDtoValidator()
        {

            RuleFor(x => x.RefreshToken)
                .NotNull().WithQuizErrorCode(MessageCodes.Required)
                .NotEmpty().WithQuizErrorCode(MessageCodes.Required);
        }
    }
}
