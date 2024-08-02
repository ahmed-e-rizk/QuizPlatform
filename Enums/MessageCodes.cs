using System.ComponentModel;

namespace QuizPlatform.Enums
{
    public enum MessageCodes
    {
        [Description("Failed : Email Already Exists")]
        AlreadyExists = 1000,
        [Description("Failed : Name isn`t Empty")]
        NameNotEmpty = 1001,
        [Description("Failed :  dosn`t Email")]

        InvalidEmail = 1002,
        [Description("Failed :  Password dosn`t Match")]

        NotMatchPass = 1003,
        [Description("Failed :  Required")]

        Required = 1004,
        [Description("Failed :Invalid login credentials")]

        InvalidLoginCredentials = 1005,
        [Description("Failed :Invalid Token")]

        InvalidToken = 1006,
        [Description("Failed :Not Found")]

        NotFound = 1007,
        [Description("Failed :Tokens DoNot Match")]

        TokensDoNotMatch = 1008
    }
}
