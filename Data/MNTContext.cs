using JTable_test2.Models;
using Microsoft.EntityFrameworkCore;

namespace JTable_test2.Data
{
    public class MNTContext :DbContext
    {
        public MNTContext(DbContextOptions<MNTContext> options): base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }

    }
}
