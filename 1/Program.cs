using _1.Models;
using _1.Models.Configuration;
using _1.Services;
using Microsoft.Extensions.DependencyInjection;
using static _1.Services.ServiceUser;


var serviceProvider = ServiceConfiguration.ConfigureServices();

var app = serviceProvider.GetRequiredService<App>();
app.Run();