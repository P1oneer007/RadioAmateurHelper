using System.ComponentModel.DataAnnotations;

namespace RadioAmateurHelper.Models
{
    public class ComponentModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public static class ComponentDataStore
    {
        public static List<ComponentModel> All => new()
        {
            new ComponentModel { Name = "BC547", Type = "NPN транзистор", Description = "Универсальный маломощный транзистор" },
            new ComponentModel { Name = "TL431", Type = "Стабилитрон", Description = "Прецизионный регулируемый" },
            new ComponentModel { Name = "NE555", Type = "Таймер", Description = "Широко используемый таймер в схемах" },
        };
    }
}
