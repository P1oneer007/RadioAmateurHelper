using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace RadioAmateurHelper.Pages
{
    // ��������� ����������� �������� � ������ ���������� ������ ������
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    // ��������� �������� ��������� (AntiForgeryToken) � ��� �������� ������ ��� �� ���������
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; } // ������������� �������, ����� ��� ������������ ������

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); // ���������� �� RequestId �� �������� (���� �� ����)

        private readonly ILogger<ErrorModel> _logger; // ������ � ����� ������������ ��� ������ ������ � ������

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // ��� GET-������� �������� ������������� ������� ��������
            // ���� �� ���������� � ���������� HttpContext.TraceIdentifier
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
