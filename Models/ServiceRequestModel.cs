namespace RadioAmateurHelper.Models
{
    public class ServiceRequestModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Contact { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
