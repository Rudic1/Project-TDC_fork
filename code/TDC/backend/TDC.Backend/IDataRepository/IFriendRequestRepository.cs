namespace TDC.Backend.IDataRepository
{
    public interface IFriendRequestRepository
    {
        public void SendFriendRequest(string senderName, string receiverName);
        public void DeleteFriendRequest(string username, string requestName);
        public List<string> GetRequestsForUser(string username);
    }
}
