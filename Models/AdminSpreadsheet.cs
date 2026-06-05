namespace RadioAmateurHelper.Models
{
    public class AdminSpreadsheet
    {
        public int Id { get; set; }
        public string DataJson { get; set; } = "{\"headers\":[\"Наименование\",\"Кол-во\",\"Цена\",\"Сумма\",\"Примечание\"],\"rows\":[]}";
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
