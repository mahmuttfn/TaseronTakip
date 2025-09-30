using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using TaseronTakip.Models;


var builder = WebApplication.CreateBuilder(args);

// sqlite dosya yolu sabitle
var defaultCs = builder.Configuration.GetConnectionString("DefaultConnection");
var cs = string.IsNullOrWhiteSpace(defaultCs)
    ? $"Data Source={Path.Combine(builder.Environment.ContentRootPath, "app.db")}"
    : (defaultCs.Contains("Data Source=app.db", StringComparison.OrdinalIgnoreCase)
        ? $"Data Source={Path.Combine(builder.Environment.ContentRootPath, "app.db")}"
        : defaultCs);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(cs));

var app = builder.Build();

// DB init: migration varsa uygula, yoksa EnsureCreated
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        var hasPending = db.Database.GetPendingMigrations().Any();
        if (hasPending) db.Database.Migrate();
        else db.Database.EnsureCreated();
    }
    catch
    {
        db.Database.EnsureCreated();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
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

    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<UserPermission> UserPermissions => Set<UserPermission>();

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
