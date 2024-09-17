namespace beakiebot_server.Models
{
    public class UserAuthTokenResponse
    {
        public required string AccessToken { get; set; }
        public required DateTime ExpiresIn { get; set; }
        public required string RefreshToken { get; set; }
        public required List<string> Scope { get; set; }
        public required string TokenType { get; set; }
    }
}
