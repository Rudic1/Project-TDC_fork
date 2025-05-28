namespace TDC.Backend.IDomain.Models
{
    public class RewardingMessageDto
    {
        public string Message { get; set; }
        public long ListId { get; set; }

        public RewardingMessageDto() { }
        public RewardingMessageDto(string message, long listId)
        {
            Message = message;
            ListId = listId;
        }
    }
}
