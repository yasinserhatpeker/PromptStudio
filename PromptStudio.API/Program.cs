using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PromptStudio.Infrastructure.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddOpenApi();

var app = builder.Build();

builder.Services.AddDbContext<PromptStudioDbContext>(options =>
 options.UseNpgsql(builder.Configuration.GetConnectionString("PromptStudioDB"))
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        
    };
});
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
