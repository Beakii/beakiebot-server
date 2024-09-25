namespace beakiebot_server.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }

        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public User(string username, string token, string refreshToken)
        {
            
        }
    }
}
