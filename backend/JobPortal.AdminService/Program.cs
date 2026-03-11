using JobPortal.AdminService.Data;
using JobPortal.AdminService.Repositories;
using JobPortal.AdminService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;
using System.Text;

namespace JobPortal.AdminService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ─── 1. DATABASE ──────────────────────────────────────────────────────────────
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
                ));

            // ─── 2. REPOSITORIES ──────────────────────────────────────────────────────────
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IJobRepository, JobRepository>();
            builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();

            // ─── 3. SERVICES ──────────────────────────────────────────────────────────────
            builder.Services.AddScoped<ITokenService, JobPortal.AdminService.Services.TokenService>();
            builder.Services.AddScoped<IAuthService, JobPortal.AdminService.Services.AuthService>();
            builder.Services.AddScoped<IAdminService, JobPortal.AdminService.Services.AdminService>();

            // ─── 4. JWT AUTHENTICATION ────────────────────────────────────────────────────
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]!;

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretKey))
                };
            });

            // ─── 5. AUTHORIZATION ─────────────────────────────────────────────────────────
            builder.Services.AddAuthorization();

            // ─── 6. CORS ──────────────────────────────────────────────────────────────────
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // ─── 7. CONTROLLERS ───────────────────────────────────────────────────────────
            builder.Services.AddControllers();

            // ─── 8. SWAGGER ───────────────────────────────────────────────────────────────
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "JobPortal Admin API",
                    Version = "v1",
                    Description = "Admin Service for Job Portal"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter: Bearer {your JWT token here}"
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

            // ─── BUILD APP ────────────────────────────────────────────────────────────────
            var app = builder.Build();

            // ─── 9. AUTO MIGRATE DATABASE ─────────────────────────────────────────────────
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            // ─── 10. MIDDLEWARE PIPELINE ──────────────────────────────────────────────────
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "JobPortal Admin API v1");
                options.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();
            app.UseCors("AllowAngular");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
