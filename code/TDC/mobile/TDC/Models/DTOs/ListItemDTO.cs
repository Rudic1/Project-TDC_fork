using System.Text.Json.Serialization;

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

        [JsonPropertyName("itemId")]
        public long ItemId { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("isDone")]
        public bool IsDone { get; set; }
        [JsonPropertyName("finishedMembers")]
        public List<string> FinishedMembers { get; set; }
        [JsonPropertyName("effort")]
        public int Effort { get; set; }
    }
}
