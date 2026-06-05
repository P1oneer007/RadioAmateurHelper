namespace RadioAmateurHelper.Models
{
    public class ExchangeEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public decimal? Price { get; set; }
        public DateTime PostedAt { get; set; } = DateTime.UtcNow;
    }
}
