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
            Console.WriteLine(user.DisplayName);
            _userContext.Add(user);
            _userContext.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _userContext.Users.ToList();
        }

        public async Task<User> GetUser(string login)
        {
            return _userContext.Users.FirstOrDefault(x => x.Login == login);
        }
    }
}
