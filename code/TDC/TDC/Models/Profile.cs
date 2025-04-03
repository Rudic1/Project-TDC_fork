namespace TDC.Models;
public class Profile
{
    protected long UserId { get; set; }
    protected string Username { get; set; }
    protected string Description { get; set; }

    #region constructors
    public Profile(long UserId, string Username, string Description)
    {
        this.UserId = UserId;
        this.Username = Username;
        this.Description = Description;
    }
    #endregion
}

