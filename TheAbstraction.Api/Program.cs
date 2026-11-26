
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

using TheAbstraction.Api.Middleware;
using TheAbstraction.Application;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.Common.Interfaces.Auth;
using TheAbstraction.Domain.Repositories.Command.Base;
using TheAbstraction.Infrastructure;
using TheAbstraction.Infrastructure.Data;
using TheAbstraction.Infrastructure.Repository.Command.Base;
using TheAbstraction.Infrastructure.Services;
using TheAbstraction.Infrastructure.Services.User;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. CONTROLLERS
// ==========================================
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// ==========================================
// 2. DATABASE - SQLITE
// ==========================================
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "DefaultConnection is missing."
    );

// ==========================================
// 3. JWT CONFIGURATION
// ==========================================
var key = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Jwt:Key is missing.");

var issuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("Jwt:Issuer is missing.");

var audience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException("Jwt:Audience is missing.");

var expiryMinutes = builder.Configuration["Jwt:ExpiryMinutes"]
    ?? throw new InvalidOperationException("Jwt:ExpiryMinutes is missing.");

// ==========================================
// 4. JWT AUTHENTICATION
// ==========================================
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = issuer,
                ValidAudience = audience,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key)
                ),

                ClockSkew = TimeSpan.Zero
            };
    });

// ==========================================
// 5. TOKEN GENERATOR
// ==========================================
builder.Services.AddSingleton<ITokenGenerator>(
    new TokenGenerator(
        key,
        issuer,
        audience,
        expiryMinutes
    )
);

// ==========================================
// 6. APPLICATION & INFRASTRUCTURE
// ==========================================
builder.Services.AddApplicationServices();

builder.Services.AddInfrastructure(builder.Configuration);

// ==========================================
// 7. CORS
// ==========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins("https://localhost:7219")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// ==========================================
// 8. DEPENDENCY INJECTION
// ==========================================
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddScoped(
    typeof(ICommandRepository<>),
    typeof(CommandRepository<>)
);

// ==========================================
// 9. SWAGGER + JWT
// ==========================================
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TheAbstraction API",
        Version = "v1",
        Description = "TheAbstraction API Documentation"
    });

    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter your JWT token."
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        }
    );
});

// ==========================================
// 10. BUILD APPLICATION
// ==========================================
var app = builder.Build();

// ==========================================
// 11. DATABASE MIGRATION
// ==========================================
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    await dbContext.Database.EnsureCreatedAsync();
}

// ==========================================
// 12. SWAGGER MIDDLEWARE
// ==========================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "TheAbstraction API v1"
        );

        options.RoutePrefix = "swagger";
    });
}

// ==========================================
// 13. HTTP MIDDLEWARE PIPELINE
// ==========================================
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

// ==========================================
// 14. MAP CONTROLLERS
// ==========================================
app.MapControllers();

// ==========================================
// 15. RUN APPLICATION
// ==========================================
app.Run();

public partial class Program
{
}
