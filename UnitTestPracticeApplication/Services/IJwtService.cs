namespace UnitTestPracticeApplication.Services
{
    public interface IJwtService
    {
        string GetJwtToken(string username);
    }
}
