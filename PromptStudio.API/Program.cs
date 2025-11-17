using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.RateLimiting;
using PromptStudio.Application.Mappings;
using PromptStudio.Application.Services.Collections;
using PromptStudio.Application.Services.Prompts;
using PromptStudio.Application.Services.Users;
using PromptStudio.Infrastructure.Data;
using PromptStudio.Infrastructure.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// OpenAPI
builder.Services.AddOpenApi();

// DbContext
builder.Services.AddDbContext<PromptStudioDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

// JWT Authentication
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

// Authorization (gerekli)
builder.Services.AddAuthorization();

// RateLimiting Policy eklendi
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("LoginPolicy", httpContext =>
    {
        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: ip,                        // IP başına ayrı limit
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,                     // pencere başına izin verilen istek sayısı
                Window = TimeSpan.FromMinutes(1),    // pencere süresi
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0                       // kuyruğa alma, direkt 429
            });
    });
});




// DI registrations
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(PromptProfile).Assembly);
builder.Services.AddScoped<IPromptService, PromptService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//  Burada middleware sırası önemli 
app.UseAuthentication(); // JWT doğrulaması
app.UseAuthorization(); // Yetkilendirme
app.UseRateLimiter();  

app.MapControllers();

app.Run();
