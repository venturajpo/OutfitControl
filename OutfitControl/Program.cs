using OutfitControl.Components;
using OutfitControl.Database;
using Microsoft.EntityFrameworkCore;
using OutfitControl.Database.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Inicializa componentes do Razor.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configuração do DbContext para MariaDB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Registro de repositórios
builder.Services.AddScoped<FuncionarioRepository>();
builder.Services.AddScoped<LoteRepository>();
builder.Services.AddScoped<PecaRepository>();
builder.Services.AddScoped<PecaPorPedidoRepository>();
builder.Services.AddScoped<PedidoRepository>();
builder.Services.AddScoped<RetiradaRepository>();

// Registro da classe de serviço
builder.Services.AddScoped<OutfitControlService>();

var app = builder.Build();

// Configura o HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();