using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDC.Models.DTOs
{
    internal class AccountLoadingDto
    {
        public AccountLoadingDto() { }
        public AccountLoadingDto(string username, string email, string description)
        {
            Username = username;
            Email = email;
            Description = description;
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
}
