using Microsoft.EntityFrameworkCore;
using RepositoryUnitOfWorkEFCoreSQL.Api;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register project specific services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration);

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
