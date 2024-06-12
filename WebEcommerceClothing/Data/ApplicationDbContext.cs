using Microsoft.EntityFrameworkCore;
using WebEcommerceClothing.Models;

namespace WebEcommerceClothing.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)
        {

        }
        public DbSet<AccountModel> Accounts { get; set; }
    }
}
