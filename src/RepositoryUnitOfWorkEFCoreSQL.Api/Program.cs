using Mapster;
using Microsoft.EntityFrameworkCore;
using RepositoryUnitOfWorkEFCoreSQL.Application.Configurations;
using RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces;
using RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces.Repositories;
using RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces.Services;
using RepositoryUnitOfWorkEFCoreSQL.Application.Services;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Contexts;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Mapster
builder.Services.AddMapster();
MapsterConfig.RegisterMappings();

// Register AddDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepostitory, ProductRepository>();

// Register AppServices
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync(); // Applies migrations automatically
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
