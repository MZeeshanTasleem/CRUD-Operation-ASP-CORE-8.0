using Microsoft.EntityFrameworkCore;
using WebApplicationLogin.Models.Entities;

namespace WebApplicationLogin.Data
{
    public class AppData : DbContext
    {
        public AppData(DbContextOptions<AppData> options) : base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
    }
}
