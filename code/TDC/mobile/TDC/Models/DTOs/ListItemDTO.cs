namespace TDC.Models.DTOs
{
    public class ListItemDto
    {
        public ListItemDto() {}

        public ListItemDto(long itemId, string description, bool isDone, List<string> finishedMembers, int effort)
        {
            ItemId = itemId;
            Description = description;
            IsDone = isDone;
            FinishedMembers = finishedMembers;
            Effort = effort;
        }
        public long ItemId { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public List<string> FinishedMembers { get; set; }
        public int Effort { get; set; }
    }
}
