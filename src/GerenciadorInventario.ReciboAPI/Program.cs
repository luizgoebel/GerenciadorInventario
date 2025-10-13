using GerenciadorInventario.ReciboAPI.Context;
using GerenciadorInventario.ReciboAPI.Mapping;
using GerenciadorInventario.ReciboAPI.Repository;
using GerenciadorInventario.ReciboAPI.Repository.Interface;
using GerenciadorInventario.ReciboAPI.Service;
using GerenciadorInventario.ReciboAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ReciboDbContext>(o => o.UseInMemoryDatabase("ReciboDb"));

builder.Services.AddAutoMapper(typeof(ReciboProfile).Assembly);

builder.Services.AddScoped<IReciboService, ReciboService>();
builder.Services.AddScoped<IReciboRepository, ReciboRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ReciboDbContext>();
    ctx.Database.EnsureCreated();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
