using Microsoft.Data.SqlClient;
using QuizPlatform.DTO;
using QuizPlatform.Enums;
using QuizPlatform.Helder;
using QuizPlatform.Infrastructure;
using QuizPlatform.Mapping;
using Microsoft.Extensions.DependencyInjection;
using QuizPlatform.Repository;
using QuizPlatform.BLL;
using QuizPlatform.BLL.Quiz;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<AuthSetting>(builder.Configuration.GetSection(AppsettingsEnum.AuthSetting.ToString()));
// Add services to the container.
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IDbFactory, DbFactory>(s => new DbFactory(new SqlConnection(builder.Configuration.GetConnectionString(AppsettingsEnum.QuizPlatformContext.ToString()))));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ImageStorageNameResolver>();
builder.Services.AddAutoMapper(ctf => { ctf.AddProfile<DtoToEntityMappingProfile>(); ctf.AddProfile<EntityToDtoMappingProfile>(); });
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IAuthBLL, AuthBLL>();
builder.Services.AddScoped<IQuizBLL, QuizBLL>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation  
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Quiz Api",
        Description = ".Net 7"
    });

    // To Enable authorization using Swagger (JWT)  
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});


var tokenValidationParams = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration[$"{nameof(AuthSetting)}:{nameof(AuthSetting.Jwt)}:{nameof(AuthSetting.Jwt.Secret)}"])),
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidIssuer = builder.Configuration[$"{nameof(AuthSetting)}:{nameof(AuthSetting.Jwt)}:{nameof(AuthSetting.Jwt.Issuer)}"],
    RequireExpirationTime = true,
    ClockSkew = TimeSpan.Zero // remove delay of token when expire
};

builder.Services.AddSingleton(tokenValidationParams);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
        .AddJwtBearer(jwt =>
        {
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = tokenValidationParams;
        });




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
