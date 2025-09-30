using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.IO;
using TaseronTakip.Models;

var builder = WebApplication.CreateBuilder(args);

// Tek, sabit bir SQLite yolu kullan (çalýþma dizini sapmalarýný engeller)
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "app.db");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Uygulama açýlýþýnda þemayý migration'larla uygula (EnsureCreated KULLANMA)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<CompanySettings> CompanySettings => Set<CompanySettings>();
    
    public DbSet<EvrakTurKural> EvrakTurKurallari { get; set; }
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<UserPermission> UserPermissions => Set<UserPermission>();
    public DbSet<Sirket> Sirketler { get; set; }
    public DbSet<AppointmentRequest> AppointmentRequests => Set<AppointmentRequest>();
    public DbSet<RequestedDocument> RequestedDocuments => Set<RequestedDocument>();
    public DbSet<DepartmentStaff> DepartmentStaff => Set<DepartmentStaff>();

    public DbSet<Firma> Firmalar => Set<Firma>();
    public DbSet<Bolum> Bolumler => Set<Bolum>();
    public DbSet<LogEntry> Loglar => Set<LogEntry>();

}

public class CompanySettings
{
    public int Id { get; set; }
    public string? SirketAdi { get; set; }
    public string? Telefon { get; set; }
    public string? Adres { get; set; }
    public string? Fax { get; set; }
    public string? Mail { get; set; }
    public string? TaseronDosyaYoluNo { get; set; }
    public string? SmtpHost { get; set; }
    public int? SmtpPort { get; set; }
    public string? SmtpUser { get; set; }
    public string? SmtpPassword { get; set; }
}
