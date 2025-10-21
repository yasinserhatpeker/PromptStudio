using System;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using PromptStudio.Domain.Entites;

namespace PromptStudio.Infrastructure.Data;

public class PromptStudioDbContext : DbContext
{
    public PromptStudioDbContext(DbContextOptions<PromptStudioDbContext> options) : base(options)
    {}

    public DbSet<User> Users { get; set; }
    
    public DbSet<Prompt> Prompts { get; set; }

    public DbSet<Collection> Collections{ get; set; }


}
