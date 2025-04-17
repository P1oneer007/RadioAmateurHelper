using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<CircuitModel> Circuits { get; set; }
        public DbSet<FirmwareModel> Firmwares { get; set; }
        public DbSet<ReferenceModel> References { get; set; }
        public DbSet<ServiceRequestModel> ServiceRequests { get; set; }
        public DbSet<CalculatorHistory> CalculatorHistories { get; set; }
        public DbSet<ExchangeEntry> ExchangeEntries { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<ComponentModel> Components { get; set; }

    }
}
