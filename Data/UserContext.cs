using beakiebot_server.Models;
using Microsoft.EntityFrameworkCore;

namespace beakiebot_server.Data
{
    public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
