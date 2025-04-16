using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Admin
{
    public class ServiceRequestsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ServiceRequestsModel(ApplicationDbContext context) => _context = context;

        public List<ServiceRequestModel> Requests { get; set; }

        public void OnGet()
        {
            Requests = _context.ServiceRequests.OrderByDescending(r => r.CreatedAt).ToList();
        }
    }
}
