using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Text;
using TheAbstraction.Application;
using TheAbstraction.Application.Common.Interfaces;

using TheAbstraction.Infrastructure;
using TheAbstraction.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var _key = builder.Configuration["Jwt:Key"];
var _issuer = builder.Configuration["Jwt:Issuer"];
var _audience = builder.Configuration["Jwt:Audience"];
var _expirtyMinutes = builder.Configuration["Jwt:ExpiryMinutes"];

//builder.Services.AddAutoMapper(typeof(UserMapper));
//builder.Services.AddAutoMapper(typeof(UserMapper).Assembly);


builder.Services.Configure<TheAbstraction.Infrastructure.Data.MongoDatabaseSettings>(
    builder.Configuration.GetSection("MonggoDatabaseSettings")
  );

builder.Services.AddSingleton<IMongoClient>(_ => {
    var connectionString = 
        builder
            .Configuration
            .GetSection("MonggoDatabaseSettings:ConnectionString")?
            .Value;
    return new MongoClient(connectionString);
});

builder.Services.AddSingleton<IMongoClient>(_ => {
    var settings = new MongoClientSettings()
    {
        Scheme = ConnectionStringScheme.MongoDB,
        Server = new MongoServerAddress("localhost", 27017)
    };

    return new MongoClient(settings);
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = _audience,
        ValidIssuer = _issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
        ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(_expirtyMinutes))
    };
});

builder.Services.AddSingleton<ITokenGenerator>(new TokenGenerator(_key, _issuer, _audience, _expirtyMinutes));
builder.Services.AddSingleton<IPlayerService, PlayerService>();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .WithOrigins("https://localhost:7219")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập JWT theo dạng: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
    });
});


//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    await dbContext.Database.MigrateAsync();
//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program
{
}