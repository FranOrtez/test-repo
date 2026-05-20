using System.Xml.Linq;
using test_proyect.Models;

namespace test_proyect.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public List<User> GetAll()
        {
            return _users;
        }

        public User? GetById(Guid id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public User Create(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;

            _users.Add(user);

            return user;
        }

        public bool Update(Guid id, User user)
        {
            var existingUser = GetById(id);

            if (existingUser is null)
                return false;

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.IsActive = user.IsActive;

            return true;
        }

        public bool Delete(Guid id)
        {
            var user = GetById(id);

            if (user is null)
                return false;

            _users.Remove(user);

            return true;
        }
    }
}
