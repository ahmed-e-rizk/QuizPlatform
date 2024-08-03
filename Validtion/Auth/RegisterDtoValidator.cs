using FluentValidation;
using QuizPlatform.DTO.Auth;
using QuizPlatform.Enums;
using QuizPlatform.Helder;

namespace QuizPlatform.Validtion.Auth
{
    public class RegisterDtoValidator : DtoValidationAbstractBase<RegisterDto>
    {
        public RegisterDtoValidator()
        {

            RuleFor(e => e.Name).NotEmpty().WithQuizErrorCode(MessageCodes.NameNotEmpty);
            RuleFor(x => x.Email)
               .NotNull().NotEmpty().EmailAddress().WithQuizErrorCode(MessageCodes.InvalidEmail);
            RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(5).MaximumLength(20).WithQuizErrorCode(MessageCodes.NotMatchPass);
        }
    }
}
