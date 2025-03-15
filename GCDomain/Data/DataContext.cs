using GCDomain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace GCDomain.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public virtual DbSet<User> Users { get; set; }
    }
}
