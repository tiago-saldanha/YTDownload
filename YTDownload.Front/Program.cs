using YTDownload.CrossCutting.Ioc;
using YTDownload.Front.Components;
using YTDownload.Front.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ProvideServices();

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

app.ConfigureApp();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
