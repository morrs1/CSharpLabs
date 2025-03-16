using CSharpLabs.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpLabs.DB;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Animal> Animal { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Enclosure> Enclosure { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Связь Person 1-ко-многим Animal (необязательная)
        modelBuilder.Entity<Person>()
            .HasMany(p => p.Animals)
            .WithOne(a => a.Person)
            .HasForeignKey(a => a.PersonId)
            .IsRequired(false)  // Делаем связь необязательной
            .OnDelete(DeleteBehavior.SetNull);  // При удалении Person -> Animal.PersonId = NULL

        // Связь Animal 1-к-1 Enclosure (необязательная)
        modelBuilder.Entity<Animal>()
            .HasOne(a => a.Enclosure)
            .WithOne(e => e.Animal)
            .HasForeignKey<Enclosure>(e => e.AnimalId)
            .IsRequired(false)  // Делаем связь необязательной
            .OnDelete(DeleteBehavior.SetNull);  // При удалении Animal -> Enclosure.AnimalId = NULL
    }
}