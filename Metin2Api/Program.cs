using Metin2Api.Application.Services;
using Metin2Api.Domain.Repositories;
using Metin2Api.Infrastructure.Repositories;
using Metin2Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("Metin2Api.Infrastructure")));


builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IGuildRepository, GuildRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IGuildService, GuildService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    using (var scope = app.Services.CreateScope())
    {
        Console.WriteLine("Applying migrations");
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
        Console.WriteLine("Migrations applied successfully");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error during migration " + ex);
    throw;
}

app.Run();
