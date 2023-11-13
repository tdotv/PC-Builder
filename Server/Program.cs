using System.Data;
using System.Data.SqlClient;
using Dapper;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


#region CORS settings for API
builder.Services.AddCors(options => 
{
    options.AddPolicy(name: "_myAllowSpecificOrigins", policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Dependency Ijections
builder.Services.AddScoped<ISocketService, SocketService>();
builder.Services.AddScoped<DbService>();

// Map the column name with underscores to the model class
DefaultTypeMap.MatchNamesWithUnderscores = true;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("_myAllowSpecificOrigins");

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
