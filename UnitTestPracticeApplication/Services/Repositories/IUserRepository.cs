using UnitTestPracticeApplication.Models;

namespace UnitTestPracticeApplication.Services.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string username);
        void SaveUser(User user);
    }
}
