using Microsoft.EntityFrameworkCore;

namespace SignalRLab.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public virtual DbSet<Products> Products { get; set; }
    }
}
