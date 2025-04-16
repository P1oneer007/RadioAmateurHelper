using System.ComponentModel.DataAnnotations;

namespace RadioAmateurHelper.Models
{
    public class CalculatorModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Formula { get; set; } // формула как текст или даже в LaTeX

        public string JsFunction { get; set; } // можно хранить JS-функцию, если нужен динамический расчёт

        public bool IsEnabled { get; set; } = true;
    }
}
