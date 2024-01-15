namespace UnitTestPracticeApplication.DTOs
{
    public class LoginResponseDto : ResponseDto
    {
        public string Token { get; set; }

        public LoginResponseDto(bool isSuccess, string message, string token) : base(isSuccess, message)
        {
            Token = token;
        }

    }
}
