using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var dataDirectory = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
Directory.CreateDirectory(dataDirectory);

var databasePath = Path.Combine(dataDirectory, "radiohelper.db");
var legacyDatabasePath = Path.Combine(builder.Environment.ContentRootPath, "radiohelper.db");
if (File.Exists(legacyDatabasePath) && !File.Exists(databasePath))
{
    File.Copy(legacyDatabasePath, databasePath);
}

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={databasePath}"));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireAssertion(context =>
            string.Equals(context.User.Identity?.Name, "Leka-07@bk.ru", StringComparison.OrdinalIgnoreCase)));
});
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin", "AdminOnly");

    options.Conventions.AuthorizePage("/Blog/Create");
    options.Conventions.AuthorizePage("/Blog/Edit", "AdminOnly");

    options.Conventions.AuthorizePage("/Circuits/Create", "AdminOnly");
    options.Conventions.AuthorizePage("/Circuits/Edit", "AdminOnly");
    options.Conventions.AuthorizePage("/Circuits/Delete", "AdminOnly");

    options.Conventions.AuthorizePage("/Components/Create", "AdminOnly");
    options.Conventions.AuthorizePage("/Components/Edit", "AdminOnly");
    options.Conventions.AuthorizePage("/Components/Delete", "AdminOnly");

    options.Conventions.AuthorizePage("/Firmwares/Create", "AdminOnly");
    options.Conventions.AuthorizePage("/Firmwares/Edit", "AdminOnly");
    options.Conventions.AuthorizePage("/Firmwares/Delete", "AdminOnly");

    options.Conventions.AuthorizePage("/References/Create", "AdminOnly");
    options.Conventions.AuthorizePage("/References/Edit", "AdminOnly");
    options.Conventions.AuthorizePage("/References/Delete", "AdminOnly");
});


var app = builder.Build();
app.Map("/healthz", () => Results.Ok("Healthy"));



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");   
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "uploads")),
    RequestPath = "/uploads"
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    Console.WriteLine("Applying EF Core migrations...");
    db.Database.Migrate();
    Console.WriteLine("EF Core migrations applied successfully.");
    DbInitializer.Initialize(db);
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
