using GerenciadorInventario.WEB.Clients;
using GerenciadorInventario.WEB.Clients.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// HttpClient typed clients for backend APIs
var gatewayBase = builder.Configuration["ServiceUrls:Gateway"] ?? "http://localhost:5149/api/";

builder.Services.AddHttpClient<IProdutoApiClient, ProdutoApiClient>(c =>
{
    c.BaseAddress = new Uri(gatewayBase);
});

builder.Services.AddHttpClient<IEstoqueApiClient, EstoqueApiClient>(c =>
{
    c.BaseAddress = new Uri(gatewayBase);
});

builder.Services.AddHttpClient<IPedidoApiClient, PedidoApiClient>(c =>
{
    c.BaseAddress = new Uri(gatewayBase);
});

builder.Services.AddHttpClient<IFaturamentoApiClient, FaturamentoApiClient>(c =>
{
    c.BaseAddress = new Uri(gatewayBase);
});

builder.Services.AddHttpClient<IReciboApiClient, ReciboApiClient>(c =>
{
    c.BaseAddress = new Uri(gatewayBase);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
