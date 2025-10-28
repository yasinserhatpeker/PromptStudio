using System;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using PromptStudio.Domain.Entites;

namespace PromptStudio.Infrastructure.Data;

public class PromptStudioDbContext : DbContext
{
    

    public DbSet<User> Users { get; set; }

    public DbSet<Prompt> Prompts { get; set; }

    public DbSet<PromptCollection> Collections { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Prompt>()
          
          .HasOne(p => p.User)
          .WithMany(p => p.Prompts)
          .HasForeignKey(p => p.UserId)
          
          .OnDelete(DeleteBehavior.Cascade);
    }


    public PromptStudioDbContext(DbContextOptions<PromptStudioDbContext> options) : base(options)
    {
    }


  
}
