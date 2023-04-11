using DataAccess;
using E_Book_Library.Helper;
using E_Book_Library.Models;
using Ebook.Service.Services.Implementations;
using Ebook.Service.Services.Interfases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());//automapper injected

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
//JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();//

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey

    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                          new string[] {}
                    }
                });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();//always first

app.UseAuthorization();

app.MapControllers();

app.Run();
