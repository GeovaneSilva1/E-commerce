using LojaVirtual.Web.Services;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("CatalogoAPI", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:CatalogoAPI"]);
});

builder.Services.AddHttpClient("Identity", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:Identity"]);
});


builder.Services.AddTransient<IProdutoService, ProdutoService>();
builder.Services.AddTransient<ICategoriaService, CategoriaService>();
builder.Services.AddTransient<IMarcaService, MarcaService>();
builder.Services.AddTransient<IImagemProdutoService, ImagemProdutoService>();
builder.Services.AddTransient<IAccountService, AccountService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie() 
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Authority = builder.Configuration["ServiceUri:Identity"]; // URL do backend (Identity)
    options.ClientId = builder.Configuration["AuthServer:ClientId"];
    options.ClientSecret = builder.Configuration["AuthServer:ClientSecret"];
    options.ResponseType = OpenIdConnectResponseType.Code;

    // salva o token para chamar APIs do backend
    options.SaveTokens = true;

    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
