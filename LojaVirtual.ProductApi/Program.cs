using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Services;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var mysqlServerConnection = "";

if (Environment.MachineName.Equals("DESKTOP-5CHSN2N"))
    mysqlServerConnection = builder.Configuration.GetConnectionString("duda");
else
    mysqlServerConnection = builder.Configuration.GetConnectionString("localhost");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(mysqlServerConnection));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
builder.Services.AddTransient<ICondicaoPagamentoRepository, CondicaoPagamentoRepository>();
builder.Services.AddTransient<IVendaRepository, VendaRepository>();
builder.Services.AddTransient<IVendaItemRepository, VendaItemRepository>();
builder.Services.AddTransient<ITabelaPrecoRepository, TabelaPrecoRepository>();
builder.Services.AddTransient<IPrecoProdutoClienteRepository, PrecoProdutoClienteRepository>();

builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<IProdutoService, ProdutoService>();
builder.Services.AddTransient<ITabelaPrecoService, TabelaPrecoService>();
builder.Services.AddTransient<ICondicaoPagamentoService, CondicaoPagamentoService>();
builder.Services.AddTransient<IVendaService, VendaService>();
builder.Services.AddTransient<IVendaItemService, VendaItemService>();
builder.Services.AddTransient<IPrecoProdutoClienteService, PrecoProdutoClienteService>();

builder.Services.AddTransient<ICompraService, CompraService>();

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
