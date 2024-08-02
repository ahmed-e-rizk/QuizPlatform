using FluentValidation;
using QuizPlatform.Enums;

namespace QuizPlatform.Helder
{
    public static class FluentExtensions
    {

        public static IRuleBuilderOptions<T, TProperty> WithQuizErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, MessageCodes messageCode)
        {
            return ruleBuilder.Configure(cfg =>
            {
                cfg.Current.ErrorCode = Convert.ToInt32(messageCode).ToString();
                cfg.Current.SetErrorMessage( messageCode.GetDescription());
            });
        }
       
    }
}
