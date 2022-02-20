using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApplicationCore.Dto
{
    public class UserDto
    {
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
    }
}
