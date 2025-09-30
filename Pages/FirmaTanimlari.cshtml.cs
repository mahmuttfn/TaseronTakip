using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaseronTakip.Models;

namespace TaseronTakip.Pages;

public class FirmaTanimlariModel : PageModel
{
    private readonly AppDbContext _db;
    public FirmaTanimlariModel(AppDbContext db) => _db = db;

    public List<Firma> Records { get; set; } = new();

    [BindProperty] public InputModel Input { get; set; } = new();

    public class InputModel
    {
        public int Id { get; set; }
        [Required, MaxLength(200)] public string Ad { get; set; } = "";
        [MaxLength(20)] public string? VergiNo { get; set; }
        [MaxLength(150)] public string? Yetkili { get; set; }
        [MaxLength(50)] public string? Telefon { get; set; }
        [EmailAddress, MaxLength(200)] public string? Eposta { get; set; }
        [MaxLength(400)] public string? Adres { get; set; }
        public bool Aktif { get; set; } = true;
    }

    public async Task OnGetAsync(int? id)
    {
        Records = await _db.Firmalar.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        if (id is int editId)
        {
            var f = await _db.Firmalar.AsNoTracking().FirstOrDefaultAsync(x => x.Id == editId);
            if (f != null)
            {
                Input = new InputModel
                {
                    Id = f.Id,
                    Ad = f.Ad,
                    VergiNo = f.VergiNo,
                    Yetkili = f.Yetkili,
                    Telefon = f.Telefon,
                    Eposta = f.Eposta,
                    Adres = f.Adres,
                    Aktif = f.Aktif
                };
            }
        }
    }

    public async Task<IActionResult> OnPostSaveAsync()
    {
        Records = await _db.Firmalar.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        if (!ModelState.IsValid) return Page();

        Firma e;
        if (Input.Id > 0)
        {
            e = await _db.Firmalar.FindAsync(Input.Id) ?? throw new InvalidOperationException("Kayýt bulunamadý.");
        }
        else
        {
            e = new Firma();
            _db.Firmalar.Add(e);
        }

        e.Ad = Input.Ad.Trim();
        e.VergiNo = Input.VergiNo?.Trim();
        e.Yetkili = Input.Yetkili?.Trim();
        e.Telefon = Input.Telefon?.Trim();
        e.Eposta = Input.Eposta?.Trim();
        e.Adres = Input.Adres?.Trim();
        e.Aktif = Input.Aktif;

        await _db.SaveChangesAsync();
        TempData["Ok"] = Input.Id > 0 ? "Güncellendi." : "Kaydedildi.";

        return RedirectToPage("./FirmaTanimlari");
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var e = await _db.Firmalar.FindAsync(id);
        if (e != null)
        {
            _db.Firmalar.Remove(e);
            await _db.SaveChangesAsync();
            TempData["Ok"] = "Kayýt silindi.";
        }
        return RedirectToPage("./FirmaTanimlari");
    }
}
