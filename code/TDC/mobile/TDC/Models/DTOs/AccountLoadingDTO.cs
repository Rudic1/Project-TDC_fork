using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TDC.Models.DTOs
{
    public class AccountLoadingDto
    {
        public AccountLoadingDto() { }
        public AccountLoadingDto(string username, string email, string description)
        {
            Username = username;
            Email = email;
            Description = description;
        }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
