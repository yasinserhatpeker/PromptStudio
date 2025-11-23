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
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);
var allowedOrigin = builder.Configuration["AllowedOrigins"];

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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",policy =>
    {
        if(builder.Environment.IsDevelopment())
        {
            policy.AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials();   // Only for development for security risks
        }
        else
        {
            policy.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(
               allowedOrigin!
            )
            .AllowCredentials(); // For deployment 
        }
    });
});
builder.Services.AddAutoMapper(typeof(PromptProfile).Assembly);
builder.Services.AddScoped<IPromptService, PromptService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c=>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{
            Title="PromptStudio API",
            Version="v1",
        });

        // JWT Security definition
        c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name="Authorization",
            Type=Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme="bearer",
            BearerFormat="JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description="Enter bearer {your token}"
        });

        c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type= Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

    }
);

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

//  Burada middleware sırası önemli 
app.UseAuthentication(); // JWT doğrulaması
app.UseAuthorization(); // Yetkilendirme
app.UseRateLimiter();  

app.MapControllers();

app.Run();
