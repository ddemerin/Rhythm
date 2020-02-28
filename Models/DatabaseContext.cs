using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Rhythm.Models
{
  public partial class DatabaseContext : DbContext
  {
    // Add Database tables here
    public DbSet<Band> Bands { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Song> Songs { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
// #error Make sure to update the connection strin gto the correct database
        optionsBuilder.UseNpgsql("server=localhost;database=RhythmDatabase");
      }
    }
  }
}
