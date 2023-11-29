using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PC_Designer.Client;
using PC_Designer.ViewModels;
using PC_Designer.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.Toast;
using Blazored.LocalStorage;
using PC_Designer.Client.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddTransient<CustomAuthorizationHandler>();

builder.Services.AddBlazoredToast();

// Dependency Ijections
builder.Services.AddTransient<IRegisterViewModel, RegisterViewModel>();
builder.Services.AddTransient<ILoginViewModel, LoginViewModel>();
builder.Services.AddTransient<IProfileViewModel, ProfileViewModel>();
builder.Services.AddScoped<ISocketService, ClientSocketService>();
builder.Services.AddTransient<IAccessTokenService, WebAppAccessTokenService>();

// builder.Services.AddHttpClient<IProfileViewModel, ProfileViewModel>().AddHttpMessageHandler<CustomAuthorizationHandler>();
// builder.Services.AddHttpClient<ILoginViewModel, LoginViewModel>().AddHttpMessageHandler<CustomAuthorizationHandler>();
// builder.Services.AddHttpClient<IRegisterViewModel, RegisterViewModel>().AddHttpMessageHandler<CustomAuthorizationHandler>();

await builder.Build().RunAsync();
