using Exams.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configure swagger
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

//DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

//Use swagger
app.UseSwagger();
app.UseSwaggerUI();

await app.RunAsync();