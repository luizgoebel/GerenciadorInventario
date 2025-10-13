using GerenciadorInventario.EstoqueAPI.Context;
using GerenciadorInventario.EstoqueAPI.Mapping;
using GerenciadorInventario.EstoqueAPI.Repository;
using GerenciadorInventario.EstoqueAPI.Repository.Interface;
using GerenciadorInventario.EstoqueAPI.Service;
using GerenciadorInventario.EstoqueAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EstoqueDbContext>(o => o.UseInMemoryDatabase("EstoqueDb"));

builder.Services.AddAutoMapper(typeof(EstoqueProfile).Assembly);

builder.Services.AddScoped<IEstoqueRepository, EstoqueRepository>();
builder.Services.AddScoped<IEstoqueService, EstoqueService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<EstoqueDbContext>();
    ctx.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
