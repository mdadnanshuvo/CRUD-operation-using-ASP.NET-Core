using ASPCrud.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPCrud.Data
{
    public class DbClass : DbContext
    {
        public DbClass(DbContextOptions options) : base(options) 
        {
                
        }

        public DbSet<Employee> Employees { get; set; }

    }
}
