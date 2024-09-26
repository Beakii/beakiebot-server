using System.Text.Json.Serialization;

namespace beakiebot_server.Models.TwitchResponse
{
    public class GetUserResponse
    {
        [JsonPropertyName("data")]
        public required List<GetUserResponseData> Data { get; set; }
    }

    public class GetUserResponseData
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("login")]
        public required string Login { get; set; }

        [JsonPropertyName("display_name")]
        public required string DisplayName { get; set; }

        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("broadcaster_type")]
        public required string BroadcasterType { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }

        [JsonPropertyName("profile_image_url")]
        public required string ProfileImageUrl { get; set; }

        [JsonPropertyName("offline_image_url")]
        public required string OfflineImageUrl { get; set; }

        [JsonPropertyName("view_count")]
        public required int ViewCount { get; set; }

        [JsonPropertyName("email")]
        public required string Email { get; set; }

        [JsonPropertyName("created_at")]
        public required DateTime CreatedAt { get; set; }
    }
}
