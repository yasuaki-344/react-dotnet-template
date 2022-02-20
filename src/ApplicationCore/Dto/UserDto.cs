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
        public Guid id { get; set; } = new Guid();

        [Required]
        [EmailAddress]
        [JsonPropertyName("user_name")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
    }
}
