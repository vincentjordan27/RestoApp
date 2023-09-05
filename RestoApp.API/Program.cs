using RestoApp.Infrastructure;
using RestoApp.Application;
using RestoApp.Infrastructure.Auth;

using Microsoft.EntityFrameworkCore;
using Serilog;
using RestoApp.Infrastructure.Data;
using RestoApp.Application.Mapper;
using RestoApp.Application.Auth;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Debug()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IRestoAuthService, RestoAuthService>();
builder.Services.AddScoped<IRestoAuthRepository, RestoAuthRepository>();
builder.Services.AddDbContext<RestoAuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RestoDb"));
});
builder.Services.AddDbContext<RestoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RestoDb"));
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<RestoAuthDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
