using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// #region CORS settings for API
// builder.Services.AddCors(options => 
// {
//     options.AddPolicy(name: "_myAllowSpecificOrigins", policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
// });
// #endregion

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Services.AddAuthentication(options =>
    {
        //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    //.AddCookie(options => { options.LoginPath = "/user/notauthorized"; })
    .AddJwtBearer(jwtBearerOptions =>
    {
        jwtBearerOptions.RequireHttpsMetadata = true;
        jwtBearerOptions.SaveToken = true;
        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWTSettings:SecretKey"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
});

builder.Services.AddScoped<IDbService, DbService>();
// builder.Services.AddScoped<IProfileViewModel, ProfileViewModel>();
// builder.Services.AddScoped<ISocketService, ServerSocketService>();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

// Map the column name with underscores to the model class
DefaultTypeMap.MatchNamesWithUnderscores = true;

var app = builder.Build();

/*--- Logging to Database using Custom Logger Provider ---  + Need to add LogController for Dapper

ILoggerFactory loggerFactory;
loggerFactory.AddProvider(new ApplicationLoggerProvider());

IApplicationBuilder applicationBuilder;
var serviceProvider = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider;

---                                                 --- */

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

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("_myAllowSpecificOrigins");

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
