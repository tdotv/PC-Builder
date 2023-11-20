using PC_Designer.ViewModels;
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
builder.Services.AddScoped<ISocketService, ServerSocketService>();
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IProfileViewModel, ProfileViewModel>();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

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
