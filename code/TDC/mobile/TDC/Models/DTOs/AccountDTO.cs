namespace TDC.Models.DTOs
{
    public class AccountDto
    {
        public AccountDto() { }
        public AccountDto(string username, string email, string password, string description)
        {
            Username = username;
            Email = email;
            Password = password;
            Description = description;
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
    }
}
