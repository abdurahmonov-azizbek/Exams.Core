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

//Configure exposers
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

//Use swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

await app.RunAsync();