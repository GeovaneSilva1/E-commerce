using LojaVirtual.CatalogoAPI.Context;
using LojaVirtual.CatalogoAPI.Infraestrutura;
using LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces;
using LojaVirtual.CatalogoAPI.Services;
using LojaVirtual.CatalogoAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
else if (Environment.MachineName.Equals("BNU-NT005742"))
    mysqlServerConnection = builder.Configuration.GetConnectionString("emp");
else
    mysqlServerConnection = builder.Configuration.GetConnectionString("localhost");

builder.Services.AddDbContext<AppDbContextCatalogoApi>(options => options.UseSqlServer(mysqlServerConnection));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IMarcaRepository, MarcaRepository>();
builder.Services.AddTransient<IImagemProdutoRepository, ImagemProdutoRepository>();

builder.Services.AddTransient<IProdutoService, ProdutoService>();
builder.Services.AddTransient<ICategoriaService, CategoriaService>();
builder.Services.AddTransient<IMarcaService, MarcaService>();
builder.Services.AddTransient<IImagemProdutoService, ImagemProdutoService>();


// Configuração explícita das URLs para evitar conflito de portas
builder.WebHost.UseUrls("https://localhost:7024");

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
