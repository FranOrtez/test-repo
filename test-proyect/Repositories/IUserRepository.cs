using test_proyect.Models;

namespace test_proyect.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();

        User? GetById(Guid id);

        User Create(User user);

        bool Update(Guid id, User user);

        bool Delete(Guid id);
    }
}
