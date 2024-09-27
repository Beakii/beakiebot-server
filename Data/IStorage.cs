using beakiebot_server.Models;

namespace beakiebot_server.Data
{
    public interface IStorage
    {
        List<User> GetAllUsers();
        Task<User> GetUser(string displayName);
        void Add(User user);
    }
}
