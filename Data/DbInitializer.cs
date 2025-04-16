using System;
using System.Linq;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Убедимся, что база применена
            context.Database.EnsureCreated();

            // Проверка, есть ли данные
            if (context.Circuits.Any() || context.References.Any() || context.Firmwares.Any())
                return;

            // ➕ Справочные данные
            context.References.AddRange(
                new ReferenceModel
                {
                    Title = "Цветовая маркировка резисторов",
                    Content = "Цвета: Черный=0, Коричневый=1, Красный=2 и т.д.",
                    CreatedAt = DateTime.Now
                },
                new ReferenceModel
                {
                    Title = "Диапазоны частот",
                    Content = "HF = 3–30 МГц, VHF = 30–300 МГц, UHF = 300–3000 МГц и т.д.",
                    CreatedAt = DateTime.Now
                }
            );

            // ➕ Схемы
            context.Circuits.Add(
                new CircuitModel
                {
                    Title = "Простая радиосхема",
                    Description = "Схема на NE555 для мигания светодиода.",
                    ImageUrl = "/uploads/images/blinker.png",
                    SchematicFilePath = "/uploads/files/blinker.pdf",
                    CreatedAt = DateTime.Now
                }
            );

            // ➕ Прошивки
            context.Firmwares.Add(
                new FirmwareModel
                {
                    Name = "ATmega328P — прошивка blink",
                    Description = "Прошивка мигания светодиода на Arduino.",
                    FilePath = "/uploads/files/blink.hex",
                    UploadedAt = DateTime.Now
                }
            );

            context.SaveChanges();
        }
    }
}
