using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> contextOptions): base(contextOptions)
        {
        }
        
        public DbSet<Job> Jobs { get; set; }
    }
}