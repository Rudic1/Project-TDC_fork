namespace TDC.Models;
public class ListItem
{
    public ListItem(long itemId, string description, bool isDone, List<string> finishedMembers, int effort)
    {
        this.ItemId = itemId;
        this.Description = description;
        this.IsDone = isDone;
        this.FinishedMembers = finishedMembers;
        this.Effort = effort;
    }

    public ListItem(string description, int effort) {
        this.Description = description;
        this.IsDone = false;
        this.FinishedMembers = new List<string>();
        this.Effort = effort;
        this.ItemId = 0;
    }

    public long ItemId { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public List<string> FinishedMembers { get; set; }
    public int Effort { get; set; }

}
