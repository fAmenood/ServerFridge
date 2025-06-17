using Microsoft.EntityFrameworkCore;

namespace ServerFridge.DataContext
{
    public class AppDbContext:DbContext
    { 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        
    }
}
