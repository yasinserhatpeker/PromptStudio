using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PromptStudio.Infrastructure.Data;

public class PromptStudioDbContextFactory : IDesignTimeDbContextFactory<PromptStudioDbContext>
{
    public PromptStudioDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<PromptStudioDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new PromptStudioDbContext(optionsBuilder.Options);
    }
}
