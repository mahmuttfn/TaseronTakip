using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TaseronTakip.Pages;

public class KullaniciTanimlariModel : PageModel
{
    private readonly AppDbContext _db;
    public KullaniciTanimlariModel(AppDbContext db) => _db = db;

    // Liste
    public List<AppUser> Users { get; set; } = new();

    // Se�ili kullan�c� & yetki matrisi
    [BindProperty(SupportsGet = true)]
    public int? SelectedUserId { get; set; }
    public AppUser? SelectedUser { get; set; }

    [BindProperty]
    public List<PermRow> Permissions { get; set; } = new();

    // Ekle formu
    [BindProperty]
    public AddUserInput NewUser { get; set; } = new();

    public class AddUserInput
    {
        [Required, MaxLength(150)]
        public string FullName { get; set; } = "";
        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = "";
        [Required, MaxLength(50)]
        public string UserName { get; set; } = "";
        [Required, MaxLength(100)]
        public string TempPassword { get; set; } = "";
        [Required] public string Role { get; set; } = "Kullan�c�";
        public bool IsActive { get; set; } = true;
    }

    public class PermRow
    {
        public string Key { get; set; } = "";
        public string Title { get; set; } = "";
        public bool CanView { get; set; }
        public bool CanAct { get; set; }
    }

    // Uygulamada kullan�lacak men�ler
    private static readonly (string Key, string Title)[] MenuDefs = new[]
    {
        ("GENEL", "Genel Tan�mlar"),
        ("DASH", "G�sterge Paneli"),
        ("RANDEVU", "Randevu Talep"),
        ("INCELE", "�nceleme / Onay"),
        ("GIRIS", "Giri� Kontrol�"),
        ("LOG", "Log Kay�tlar�")
    };

    public async Task OnGetAsync()
    {
        // demo i�in ilk kurulum
        if (!await _db.AppUsers.AnyAsync())
        {
            var admin = new AppUser
            {
                FullName = "Sistem Y�neticisi",
                Email = "admin@example.com",
                UserName = "admin",
                TempPassword = "admin",
                Role = "Y�netici",
                IsActive = true
            };
            _db.AppUsers.Add(admin);
            await _db.SaveChangesAsync();

            foreach (var m in MenuDefs)
            {
                _db.UserPermissions.Add(new UserPermission
                {
                    UserId = admin.Id,
                    MenuKey = m.Key,
                    CanView = true,
                    CanAct = true
                });
            }
            await _db.SaveChangesAsync();
        }

        Users = await _db.AppUsers.AsNoTracking().OrderBy(x => x.Id).ToListAsync();

        SelectedUserId ??= Users.FirstOrDefault()?.Id;
        SelectedUser = await _db.AppUsers.FindAsync(SelectedUserId);
        await LoadPermissionsAsync();
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        Users = await _db.AppUsers.AsNoTracking().ToListAsync();

        if (!ModelState.IsValid) return Page();

        if (await _db.AppUsers.AnyAsync(x => x.UserName == NewUser.UserName))
        {
            ModelState.AddModelError(string.Empty, "Bu kullan�c� ad� zaten var.");
            return Page();
        }

        var u = new AppUser
        {
            FullName = NewUser.FullName.Trim(),
            Email = NewUser.Email.Trim(),
            UserName = NewUser.UserName.Trim(),
            TempPassword = NewUser.TempPassword.Trim(),
            Role = NewUser.Role,
            IsActive = NewUser.IsActive
        };
        _db.AppUsers.Add(u);
        await _db.SaveChangesAsync();

        // ilk yetki seti (hepsi kapal�)
        foreach (var m in MenuDefs)
        {
            _db.UserPermissions.Add(new UserPermission
            {
                UserId = u.Id,
                MenuKey = m.Key,
                CanView = false,
                CanAct = false
            });
        }
        await _db.SaveChangesAsync();

        TempData["Ok"] = "Kullan�c� eklendi.";
        return RedirectToPage("./KullaniciTanimlari", new { selectedUserId = u.Id });
    }

    public async Task<IActionResult> OnPostToggleActiveAsync(int id)
    {
        var u = await _db.AppUsers.FindAsync(id);
        if (u != null)
        {
            u.IsActive = !u.IsActive;
            await _db.SaveChangesAsync();
            TempData["Ok"] = u.IsActive ? "Kullan�c� aktifle�tirildi." : "Kullan�c� pasifle�tirildi.";
        }
        return RedirectToPage("./KullaniciTanimlari", new { selectedUserId = id });
    }

    public async Task<IActionResult> OnPostSavePermsAsync(int? selectedUserId)
    {
        if (selectedUserId is null) return RedirectToPage("./KullaniciTanimlari");

        var user = await _db.AppUsers.FindAsync(selectedUserId.Value);
        if (user == null) return RedirectToPage("./KullaniciTanimlari");

        // mevcut kay�tlar
        var map = await _db.UserPermissions
            .Where(x => x.UserId == user.Id)
            .ToDictionaryAsync(x => x.MenuKey, x => x);

        foreach (var p in Permissions)
        {
            if (!map.TryGetValue(p.Key, out var row))
            {
                row = new UserPermission { UserId = user.Id, MenuKey = p.Key };
                _db.UserPermissions.Add(row);
            }
            row.CanView = p.CanView;
            row.CanAct = p.CanAct;
        }

        await _db.SaveChangesAsync();
        TempData["Ok"] = "Yetkiler kaydedildi.";
        return RedirectToPage("./KullaniciTanimlari", new { selectedUserId });
    }

    private async Task LoadPermissionsAsync()
    {
        Permissions = new();
        if (SelectedUserId is null) return;

        var existing = await _db.UserPermissions
            .Where(x => x.UserId == SelectedUserId)
            .ToListAsync();

        foreach (var def in MenuDefs)
        {
            var row = existing.FirstOrDefault(x => x.MenuKey == def.Key);
            Permissions.Add(new PermRow
            {
                Key = def.Key,
                Title = def.Title,
                CanView = row?.CanView ?? false,
                CanAct = row?.CanAct ?? false
            });
        }
    }
}
