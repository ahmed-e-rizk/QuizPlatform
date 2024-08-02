using FluentValidation;

namespace QuizPlatform.Validtion
{
    public class DtoValidationAbstractBase<T> : AbstractValidator<T> where T : class
    {
    }
}
