using beakiebot_server.Models;

namespace beakiebot_server.Data
{
    public class Storage : IStorage
    {
        private readonly UserContext _userContext;

        public Storage(UserContext context)
        {
            _userContext = context;
        }

        public void Add(User user)
        {
            _userContext.Add(user);
        }

        public List<User> GetAllUsers()
        {
            return _userContext.Users.ToList();
        }
    }
}
