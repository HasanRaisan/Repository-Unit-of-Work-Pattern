using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Data.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MyConnection"),
        b => b.MigrationsAssembly("Data"))
);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddIdentityConfiguration();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    await ApplicationDbContextSeed.SeedRolesAndAdminAsync(service);
}

app.UseSwagger();
app.UseSwaggerUI();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
