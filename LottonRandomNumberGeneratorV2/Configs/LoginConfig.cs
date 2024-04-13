namespace LottonRandomNumberGeneratorV2.Configs
{
    public class LoginConfig
    {
        public const string ConfigBinding = "LoginConfig";

        public string Email { get; set; }

        public string Password { get; set; }

        public DateOfBirthConfig DateOfBirth { get; set; }
    }
}