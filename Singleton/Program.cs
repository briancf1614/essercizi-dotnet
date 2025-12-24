using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Singleton;
using Singleton.Singletons;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<SmartHomeHubSingleton>(sp =>
{
    // A. REGISTRO PRINCIPALE (La Creazione Fisica)
    // Poiché il costruttore richiede una stringa ("Villa Stark"), non possiamo usare AddSingleton<T>().
    // Usiamo una "factory lambda" (sp => ...).
    // 'sp' è lo ServiceProvider, la cassetta degli attrezzi.
    // si puo usare IOptions in casi come dev teest preprod e prod
    var logger = sp.GetRequiredService<ILogger<SmartHomeHubSingleton>>();
    return new SmartHomeHubSingleton(logger, "Villa Stark");
});

// ti chiedo la interfaccia di Ihomeviewer e tu mi restituisce la istanza singleton di Smarthomehubsingleton
// e pur sempre la stessa istanza
builder.Services.AddSingleton<IHomeViewer>(sp =>
{
    return sp.GetRequiredService<SmartHomeHubSingleton>();
});

builder.Services.AddSingleton<IHomeController>(sp =>
{
    return sp.GetRequiredService<SmartHomeHubSingleton>();
});

builder.Services.AddSingleton<ISecuritySystem>(sp =>
{
    return sp.GetRequiredService<SmartHomeHubSingleton>();
});



builder.Services.AddHostedService<LifeSimulatorWorker>();

using IHost host = builder.Build();

// E ora, invece di eseguire comandi manuali, lanciamo l'app per sempre:
await host.RunAsync();