using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.Infraestrutura;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
builder.Services.AddTransient<ICondicaoPagamentoRepository, CondicaoPagamentoRepository>();
builder.Services.AddTransient<IVendaRepository, VendaRepository>();
builder.Services.AddTransient<IVendaItemRepository, VendaItemRepository>();
builder.Services.AddTransient<ITabelaPrecoRepository, TabelaPrecoRepository>();

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
