using Microsoft.EntityFrameworkCore;
using TesteGlobalTec.Models;

namespace TesteGlobalTec.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<People> Peoples { get; set; }
        
    }
}