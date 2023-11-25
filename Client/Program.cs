using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PC_Designer.Client;
using PC_Designer.ViewModels;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
// using Microsoft.AspNetCore.Components.Authorization;
// using PC_Designer.Client.Logging;
// using PC_Designer.Toast;
// using PC_Designer.LocalStorage;
// using PC_Designer.Client.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IProfileViewModel, ProfileViewModel>();
builder.Services.AddScoped<ILoginViewModel, LoginViewModel>();

// Dependency Ijections
builder.Services.AddScoped<ISocketService, ClientSocketService>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// builder.Services.AddDapper

await builder.Build().RunAsync();
