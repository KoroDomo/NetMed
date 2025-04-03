namespace NetMedWebApi.Models.Notification
{
    public class NotificationApiModel
    {
        public int id { get; set; }
        public int userID { get; set; }
        public required string message { get; set; }
        public DateTime? sentAt { get; set; }

    }
}
