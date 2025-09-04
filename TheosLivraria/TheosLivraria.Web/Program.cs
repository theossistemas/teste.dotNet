using Blazored.LocalStorage;
using Radzen;
using Refit;
using TheosLivraria.Web.Components;
using TheosLivraria.Web.ServicesAPI;

var builder = WebApplication.CreateBuilder(args);

var baseAddress = builder.Configuration["ApiSettings:BaseUrl"];


builder.Services.AddHttpClient("IUsuarioServices", c =>
{
    c.BaseAddress = new Uri(baseAddress);
});

builder.Services.AddRefitClient<ISegurancaService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));

builder.Services.AddRefitClient<IUsuarioService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));

builder.Services.AddRefitClient<ILivroService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));



builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor();
builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredLocalStorage();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
