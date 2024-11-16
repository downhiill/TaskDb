using _1;
using _1.Models;
using _1.Models.Configuration;
using _1.Services;
using Microsoft.Extensions.DependencyInjection;

// Конфигурируем сервисы через ServiceConfiguration
var serviceProvider = ServiceConfiguration.ConfigureServices();

// Создаем приложение, инжектируя все зависимости
var app = serviceProvider.GetRequiredService<App>();

// Запускаем приложение
app.Run();
