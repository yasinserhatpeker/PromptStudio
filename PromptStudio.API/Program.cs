using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PromptStudio.Application.Mappings;
using PromptStudio.Application.Services.Collections;
using PromptStudio.Application.Services.Prompts;
using PromptStudio.Application.Services.Users;
using PromptStudio.Infrastructure.Data;
using PromptStudio.Infrastructure.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();




builder.Services.AddDbContext<PromptStudioDbContext>(options =>
 options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);


Log.Logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration)
.Enrich.FromLogContext()
.CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),

    };
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(PromptProfile).Assembly);
builder.Services.AddScoped<IPromptService, PromptService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers(); 


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
