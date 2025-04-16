namespace RadioAmateurHelper.Models
{
    public class CalculatorHistory
    {
        public int Id { get; set; }
        public string CalculatorName { get; set; }
        public string InputValues { get; set; }
        public string Result { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
