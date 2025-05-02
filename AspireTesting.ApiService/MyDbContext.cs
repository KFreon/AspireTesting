using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspireTesting.ApiService
{
    public class MyEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }    
    }

    public class MyDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<MyEntity> MyNames => Set<MyEntity>();
    }
}
