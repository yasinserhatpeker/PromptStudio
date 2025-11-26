using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PromptStudio.Infrastructure.Data;

public class PromptStudioDbContextFactory : IDesignTimeDbContextFactory<PromptStudioDbContext>
{
    public PromptStudioDbContext CreateDbContext(string[] args)
    {

        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../PromptStudio.API");
        
        // Eğer klasörü bulamazsa (belki zaten API klasöründeyizdir), mevcut dizini kullan.
        if (!Directory.Exists(basePath))
        {
            basePath = Directory.GetCurrentDirectory();
        }

        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddUserSecrets("341d95f2-556f-45cf-9a32-161a3a37980c") 
                .Build();
             

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
             throw new InvalidOperationException($"Connection string 'DefaultConnection' bulunamadı! Path: {basePath}");
        }

        var optionsBuilder = new DbContextOptionsBuilder<PromptStudioDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new PromptStudioDbContext(optionsBuilder.Options);
    }
}
