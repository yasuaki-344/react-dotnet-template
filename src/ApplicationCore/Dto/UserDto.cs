using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApplicationCore.Dto
{
    [DisplayName("User")]
    public class UserDto
    {
        [Required]
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = new Guid();

        [Required]
        [JsonPropertyName("user_name")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;
    }
}
