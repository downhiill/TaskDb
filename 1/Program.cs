using _1.Models;
using _1.Services;
using Microsoft.Extensions.DependencyInjection;
using static _1.Services.ServiceUser;

var serviceProvider = new ServiceCollection()
           .AddScoped<ServiceUsers>()           // Регистрируем ServiceUsers
           .AddScoped<ServiceRoles>()           // Регистрируем ServiceRoles
           .AddScoped<ServiceProfession>()      // Регистрируем ServiceProfession
           .AddScoped<ServiceAccount>()         // Регестрируем ServiceAccount
           .AddScoped<App>()                    // Регистрируем App
           .AddCommands()                       // Регистрируем все команды автоматически
           .BuildServiceProvider();

var app = serviceProvider.GetRequiredService<App>();
app.Run();