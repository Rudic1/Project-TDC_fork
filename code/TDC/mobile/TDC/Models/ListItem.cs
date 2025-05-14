namespace TDC.Models;
public class ListItem(long itemId, string description, bool isDone, List<string> finishedMembers, int effort)
{
    public long ItemId { get; set; } = itemId;
    public string Description { get; set; } = description;
    public bool IsDone { get; set; } = isDone;
    public List<string> FinishedMembers { get; set; } = finishedMembers;
    public int Effort { get; set; } = effort;

}
