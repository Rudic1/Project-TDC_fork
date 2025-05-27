namespace TDC.Models.DTOs
{
    public class RewardingMessageDto
    {
        public RewardingMessageDto() { }
        public RewardingMessageDto(string message, long listId)
        {
            Message = message;
            ListId = listId;
        }
        public long ListId { get; set; }
        public string Message { get; set; }
    }
}