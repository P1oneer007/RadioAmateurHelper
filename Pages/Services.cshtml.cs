using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages
{
    public class ServicesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ServicesModel(ApplicationDbContext context) => _context = context;

        [BindProperty] public string UserName { get; set; }
        [BindProperty] public string Contact { get; set; }
        [BindProperty] public string Message { get; set; }

        public string SuccessMessage { get; set; }

        public void OnGet() { }

        public void OnPost()
        {
            if (!ModelState.IsValid) return;

            _context.ServiceRequests.Add(new ServiceRequestModel
            {
                UserName = UserName,
                Contact = Contact,
                Message = Message,
                CreatedAt = DateTime.Now
            });
            _context.SaveChanges();

            SuccessMessage = "Спасибо! Ваша заявка успешно отправлена.";
        }
    }
}
