using Microsoft.EntityFrameworkCore;
using SessionVar.Models.Classes;

namespace SessionVar.Models.Data
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context>db):base(db)
        {

        }
        public DbSet<Users> Users { get; set; }
    }
}
