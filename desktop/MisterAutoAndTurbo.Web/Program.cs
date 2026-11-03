using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Prefer Supabase/Postgres when configured; otherwise use local SQLite so the app runs after clone.
var supabase = builder.Configuration.GetConnectionString("Supabase");
var useSqlite = string.IsNullOrWhiteSpace(supabase)
                || supabase.Contains("YOUR_", StringComparison.OrdinalIgnoreCase);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (useSqlite)
    {
        var sqlite = builder.Configuration.GetConnectionString("Default") ?? "Data Source=misterauto.db";
        options.UseSqlite(sqlite);
    }
    else
    {
        options.UseNpgsql(supabase);
    }
});

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<PersistChangesFilter>();
});
builder.Services.AddScoped<PersistChangesFilter>();
builder.Services.AddSingleton<IEmailService, SmtpEmailService>();
builder.Services.AddSingleton<PasswordResetService>();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.Name = "MisterAuto.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Create DB + seed manager on first run
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        AppDataStore.Initialize(db);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Startup data init: {ex.Message}");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
