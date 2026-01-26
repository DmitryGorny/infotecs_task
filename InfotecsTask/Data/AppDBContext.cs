using InfotecsTask.Models;
using Microsoft.EntityFrameworkCore;

namespace InfotecsTask.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        protected AppDBContext()
        {
        }

        public DbSet<Values> Values { get; set; }
    }
}
