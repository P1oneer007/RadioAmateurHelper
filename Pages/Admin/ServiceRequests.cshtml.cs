using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Admin
{
    [Authorize]
    public class ServiceRequestsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ServiceRequestsModel(ApplicationDbContext context) => _context = context;

        public List<ServiceRequestModel> Requests { get; set; }

        public IActionResult OnGet()
        {
            // Проверка Email пользователя
            if (User.Identity?.IsAuthenticated != true || User.Identity.Name != "Leka-07@bk.ru")
            {
                return Forbid(); // 403 Forbidden
            }

            Requests = _context.ServiceRequests.OrderByDescending(r => r.CreatedAt).ToList();
            return Page();
        }
    }
}
