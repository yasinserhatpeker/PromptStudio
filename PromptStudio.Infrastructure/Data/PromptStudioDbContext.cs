using System;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace PromptStudio.Infrastructure.Data;

public class PromptStudioDbContext : DbContext
{
    public PromptStudioDbContext(DbContextOptions<PromptStudioDbContext> options) : base(options)
    {

    }


}
