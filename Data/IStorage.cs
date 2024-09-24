using beakiebot_server.Models;

namespace beakiebot_server.Data
{
    public interface IStorage
    {
        List<User> GetAllUsers();
        void Add(User user);
    }
}
