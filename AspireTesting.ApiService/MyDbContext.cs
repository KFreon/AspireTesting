using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspireTesting.ApiService
{
    public class MyHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string RandomNumber { get; set; }    
    }

    public class MyDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<MyHistory> TheHistory => Set<MyHistory>();
    }
}
