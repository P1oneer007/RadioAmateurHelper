using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace RadioAmateurHelper.Pages
{
    // ќтключает кэширование страницы Ч всегда возвращает свежую версию
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    // ќтключает проверку антифрода (AntiForgeryToken) Ч дл€ страницы ошибок она не требуетс€
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; } // »дентификатор запроса, нужен дл€ отслеживани€ ошибки

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); // ѕоказывать ли RequestId на странице (если он есть)

        private readonly ILogger<ErrorModel> _logger; // Ћоггер Ч можно использовать дл€ записи ошибок в журнал

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // ѕри GET-запросе получаем идентификатор текущей операции
            // ≈сли он недоступен Ч используем HttpContext.TraceIdentifier
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
