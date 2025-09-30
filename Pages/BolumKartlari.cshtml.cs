using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaseronTakip.Models;

namespace TaseronTakip.Pages;

public class BolumKartlariModel : PageModel
{
    private readonly AppDbContext _db;
    public BolumKartlariModel(AppDbContext db) => _db = db;

    public List<Bolum> Records { get; set; } = new();

    [BindProperty] public InputModel Input { get; set; } = new();

    public class InputModel
    {
        public int Id { get; set; }
        [Required, MaxLength(150)] public string Ad { get; set; } = "";
        [MaxLength(30)] public string? KisaKod { get; set; }
        [MaxLength(150)] public string? Sorumlu { get; set; }
        [MaxLength(300)] public string? Aciklama { get; set; }
        public bool Aktif { get; set; } = true;
    }

    public async Task OnGetAsync(int? id)
    {
        Records = await _db.Bolumler.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();

        if (id is int editId)
        {
            var b = await _db.Bolumler.AsNoTracking().FirstOrDefaultAsync(x => x.Id == editId);
            if (b != null)
            {
                Input = new InputModel
                {
                    Id = b.Id,
                    Ad = b.Ad,
                    KisaKod = b.KisaKod,
                    Sorumlu = b.Sorumlu,
                    Aciklama = b.Aciklama,
                    Aktif = b.Aktif
                };
            }
        }
    }

    public async Task<IActionResult> OnPostSaveAsync()
    {
        Records = await _db.Bolumler.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        if (!ModelState.IsValid) return Page();

        Bolum e;
        if (Input.Id > 0)
        {
            e = await _db.Bolumler.FindAsync(Input.Id) ?? throw new InvalidOperationException("Kayýt bulunamadý.");
        }
        else
        {
            e = new Bolum();
            _db.Bolumler.Add(e);
        }

        e.Ad = Input.Ad.Trim();
        e.KisaKod = Input.KisaKod?.Trim();
        e.Sorumlu = Input.Sorumlu?.Trim();
        e.Aciklama = Input.Aciklama?.Trim();
        e.Aktif = Input.Aktif;

        await _db.SaveChangesAsync();
        TempData["Ok"] = Input.Id > 0 ? "Güncellendi." : "Kaydedildi.";
        return RedirectToPage("./BolumKartlari");
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var e = await _db.Bolumler.FindAsync(id);
        if (e != null)
        {
            _db.Bolumler.Remove(e);
            await _db.SaveChangesAsync();
            TempData["Ok"] = "Kayýt silindi.";
        }
        return RedirectToPage("./BolumKartlari");
    }
}
