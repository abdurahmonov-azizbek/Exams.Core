using Exams.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Exams.Application;

var builder = WebApplication.CreateBuilder(args);

//Configure swagger
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

//DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Configure services
builder.Services.AddServices();

var app = builder.Build();

//Use swagger
app.UseSwagger();
app.UseSwaggerUI();

await app.RunAsync();