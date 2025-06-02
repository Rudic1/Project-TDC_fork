namespace TDC.Models
{
    public class MemberWithPoints
    {
        public string Username { get; set; }
        public int Points { get; set; }

        public MemberWithPoints() { }

        public MemberWithPoints(string username, int points)
        {
            Username = username;
            Points = points;
        }
    }
}
