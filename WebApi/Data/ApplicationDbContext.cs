using Microsoft.EntityFrameworkCore;
using WebApi.Models;
namespace WebApi.Data;

public class ApplicationDbContext : DbContext
{       
    //options comes from the program.cs
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Shirt> Shirts { get; set; }
    //DbSet represents table

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //data seeding
        modelBuilder.Entity<Shirt>().HasData(
            new Shirt { ShirtId = 1, Brand = "My Brand", Color = "Blue", Gender = "Men", Price = 30, Size = 10 },
            new Shirt { ShirtId = 2, Brand = "My Brand", Color = "Black", Gender = "Men", Price = 35, Size = 12 },
            new Shirt { ShirtId = 3, Brand = "Your Brand", Color = "Pink", Gender = "Women", Price = 28, Size = 8 },
            new Shirt { ShirtId = 4, Brand = "Your Brand", Color = "Yellow", Gender = "Women", Price = 30, Size = 9 }
            );
    }
}