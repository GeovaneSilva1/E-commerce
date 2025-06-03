using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Endpoints produtos
app.MapGet("/api/produtos", async (AppDbContext db) =>
    await db.Produtos.ToListAsync());

app.MapGet("/api/produtos/{id}", async (int id, AppDbContext db) =>
    await db.Produtos.FindAsync(id)
        is Produto produto
            ? Results.Ok(produto)
            : Results.NotFound());

app.MapPost("/api/produtos", async (Produto produto, AppDbContext db) =>
{
    db.Produtos.Add(produto);
    await db.SaveChangesAsync();
    return Results.Created($"/api/produtos/{produto.Id}", produto);
});

app.MapPut("/api/produtos/{id}", async (int id, Produto input, AppDbContext db) =>
{
    var produto = await db.Produtos.FindAsync(id);
    if (produto is null) return Results.NotFound();

    produto.SKU = input.SKU;
    produto.Descricao = input.Descricao;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/produtos/{id}", async (int id, AppDbContext db) =>
{
    var produto = await db.Produtos.FindAsync(id);
    if (produto is null) return Results.NotFound();

    db.Produtos.Remove(produto);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

//EndPoints Clientes
// Endpoints
app.MapGet("/api/clientes", async (AppDbContext db) =>
    await db.Clientes.ToListAsync());

app.MapGet("/api/clientes/{id}", async (int id, AppDbContext db) =>
    await db.Clientes.FindAsync(id)
        is Cliente cliente
            ? Results.Ok(cliente)
            : Results.NotFound());

app.MapPost("/api/clientes", async (Cliente cliente, AppDbContext db) =>
{
    db.Clientes.Add(cliente);
    await db.SaveChangesAsync();
    return Results.Created($"/api/clientes/{cliente.Id}", cliente);
});

app.MapPut("/api/clientes/{id}", async (int id, Cliente input, AppDbContext db) =>
{
    var cliente = await db.Clientes.FindAsync(id);
    if (cliente is null) return Results.NotFound();

    cliente.CNPJ = input.CNPJ;
    cliente.RazaoSocial = input.RazaoSocial;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/clientes/{id}", async (int id, AppDbContext db) =>
{
    var cliente = await db.Clientes.FindAsync(id);
    if (cliente is null) return Results.NotFound();

    db.Clientes.Remove(cliente);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Endpoints TabelaPreco
app.MapGet("/api/tabelaspreco", async (AppDbContext db) =>
    await db.TabelaPrecos.ToListAsync());

app.MapPost("/api/tabelaspreco", async (TabelaPreco tabela, AppDbContext db) =>
{
    db.TabelaPrecos.Add(tabela);
    await db.SaveChangesAsync();
    return Results.Created($"/api/tabelaspreco/{tabela.Id}", tabela);
});

// Endpoints PrecoProdutoCliente
app.MapGet("/api/precos", async (AppDbContext db) =>
    await db.PrecoProdutoClientes.ToListAsync());

app.MapPost("/api/precos", async (PrecoProdutoCliente preco, AppDbContext db) =>
{
    db.PrecoProdutoClientes.Add(preco);
    await db.SaveChangesAsync();
    return Results.Created($"/api/precos/{preco.Id}", preco);
});

// Endpoints de Venda
app.MapGet("/api/vendas", async (AppDbContext db) =>
    await db.Vendas.Include(v => v.Itens).ToListAsync());

app.MapGet("/api/vendas/{id}", async (int id, AppDbContext db) =>
    await db.Vendas.Include(v => v.Itens).FirstOrDefaultAsync(v => v.Id == id)
        is Venda venda
            ? Results.Ok(venda)
            : Results.NotFound());

app.MapPost("/api/vendas", async (Venda venda, AppDbContext db) =>
{
    db.Vendas.Add(venda);
    await db.SaveChangesAsync();
    return Results.Created($"/api/vendas/{venda.Id}", venda);
});

app.MapDelete("/api/vendas/{id}", async (int id, AppDbContext db) =>
{
    var venda = await db.Vendas.FindAsync(id);
    if (venda is null) return Results.NotFound();

    db.Vendas.Remove(venda);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Endpoint de Relatório por Cliente
app.MapGet("/api/relatorio/{clienteId}", async (int clienteId, AppDbContext db) =>
{
    var vendas = await db.Vendas
        .Include(v => v.Itens)
        .Where(v => v.ClienteId == clienteId)
        .ToListAsync();

    return Results.Ok(vendas);
});

// Endpoints de Condição de Pagamento
app.MapGet("/api/condicoes", async (AppDbContext db) =>
    await db.CondicaoPagamentos.ToListAsync());

app.MapPost("/api/condicoes", async (CondicaoPagamento cond, AppDbContext db) =>
{
    db.CondicaoPagamentos.Add(cond);
    await db.SaveChangesAsync();
    return Results.Created($"/api/condicoes/{cond.Id}", cond);
});

app.UseAuthorization();

app.MapControllers();

app.Run();
