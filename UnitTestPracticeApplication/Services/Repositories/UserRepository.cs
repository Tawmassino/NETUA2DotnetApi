using System.Linq;
using UnitTestPracticeApplication.Models;

namespace UnitTestPracticeApplication.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUser(string username)
        {
            return _context.Users.SingleOrDefault(x => x.Username == username);
        }

        public void SaveUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
