namespace beakiebot_server.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public required string DisplayName { get; set; }
        public required string ProfileImageUrl { get; set; }
        public required string Email { get; set; }
        public required string AuthToken { get; set; }
        public required string RefreshToken { get; set; }
        public required int ExpiresIn { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime LastUpdatedAt { get; set; }
    }
}
