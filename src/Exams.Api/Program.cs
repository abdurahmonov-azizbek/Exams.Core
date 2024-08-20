using Exams.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Exams.Application;
using Microsoft.OpenApi.Models;
using Exams.Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Exams.Api.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);

//Configure swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "Enter valid token"
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

//DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Configure services
builder.Services.AddServices();

//Configure exposers
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Add JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),
            RoleClaimType = nameof(Role)
        };
    }
);

builder.Services.AddCors(options => options.AddPolicy("AllowAll", policy =>
{
    policy
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
}));

var app = builder.Build();

//Use swagger
app.UseSwagger();
app.UseSwaggerUI();

//Use custom middlewares
app.UseMiddleware<ExceptionHandlingMiddleware>();

//Use cors
app.UseCors("AllowAll");

//configure auth
app.UseAuthentication();
app.UseAuthorization();

//Http redirection
app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();