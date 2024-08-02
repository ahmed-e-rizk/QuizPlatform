using FluentValidation;
using QuizPlatform.DTO.Auth;
using QuizPlatform.Enums;
using QuizPlatform.Helder;

namespace QuizPlatform.Validtion.Auth
{
    public class LoginDtoValidator : DtoValidationAbstractBase<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithQuizErrorCode(MessageCodes.Required)
                .NotEmpty().WithQuizErrorCode(MessageCodes.Required)
                .EmailAddress().WithQuizErrorCode(MessageCodes.InvalidEmail);

            RuleFor(x => x.Password)
                .NotNull().WithQuizErrorCode(MessageCodes.Required)
                .NotEmpty().WithQuizErrorCode(MessageCodes.Required);
        }

    }
}
