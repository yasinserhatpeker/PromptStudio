using Microsoft.EntityFrameworkCore;
using PromptStudio.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

builder.Services.AddDbContext<PromptStudioDbContext>(options =>
 options.UseNpgsql(builder.Configuration.GetConnectionString("PromptStudioDB"))
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
