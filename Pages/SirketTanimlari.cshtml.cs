using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TaseronTakip.Pages;

public class SirketTanimlariModel : PageModel
{
    private readonly AppDbContext _db;
    public SirketTanimlariModel(AppDbContext db) => _db = db;

    public List<CompanySettings> Records { get; set; } = new();

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        public int Id { get; set; }

        [Display(Name = "Şirket Adı"), MaxLength(200)]
        public string? SirketAdi { get; set; }

        [MaxLength(50)]
        public string? Telefon { get; set; }

        [MaxLength(400)]
        public string? Adres { get; set; }

        [MaxLength(50)]
        public string? Fax { get; set; }

        [EmailAddress, MaxLength(200)]
        public string? Mail { get; set; }

        [Display(Name = "Taşeron Dosya Yolu/No"), MaxLength(400)]
        public string? TaseronDosyaYoluNo { get; set; }

        [Display(Name = "Mail Sunucu"), MaxLength(200)]
        public string? SmtpHost { get; set; }

        [Range(1, 65535)]
        public int? SmtpPort { get; set; }

        [Display(Name = "Kullanıcı"), MaxLength(200)]
        public string? SmtpUser { get; set; }

        [Display(Name = "Şifre"), MaxLength(400)]
        public string? SmtpPassword { get; set; }
    }

    public async Task OnGetAsync(int? id)
    {
        Records = await _db.CompanySettings.AsNoTracking()
                                           .OrderByDescending(x => x.Id)
                                           .ToListAsync();

        if (id is int editId)
        {
            var e = await _db.CompanySettings.AsNoTracking()
                                             .FirstOrDefaultAsync(x => x.Id == editId);
            if (e != null)
            {
                Input = new InputModel
                {
                    Id = e.Id,
                    SirketAdi = e.SirketAdi,
                    Telefon = e.Telefon,
                    Adres = e.Adres,
                    Fax = e.Fax,
                    Mail = e.Mail,
                    TaseronDosyaYoluNo = e.TaseronDosyaYoluNo,
                    SmtpHost = e.SmtpHost,
                    SmtpPort = e.SmtpPort,
                    SmtpUser = e.SmtpUser,
                    SmtpPassword = e.SmtpPassword
                };
            }
        }
    }

    public async Task<IActionResult> OnPostSaveAsync()
    {
        // Listeyi doldur (ModelState invalid olursa sayfayı tekrar çizerken lazım)
        Records = await _db.CompanySettings.AsNoTracking()
                                           .OrderByDescending(x => x.Id)
                                           .ToListAsync();

        if (!ModelState.IsValid) return Page();

        CompanySettings e;
        if (Input.Id > 0)
        {
            e = await _db.CompanySettings.FindAsync(Input.Id)
                ?? throw new InvalidOperationException("Kayıt bulunamadı.");
        }
        else
        {
            e = new CompanySettings();
            _db.CompanySettings.Add(e);
        }

        e.SirketAdi = Input.SirketAdi?.Trim();
        e.Telefon = Input.Telefon?.Trim();
        e.Adres = Input.Adres?.Trim();
        e.Fax = Input.Fax?.Trim();
        e.Mail = Input.Mail?.Trim();
        e.TaseronDosyaYoluNo = Input.TaseronDosyaYoluNo?.Trim();
        e.SmtpHost = Input.SmtpHost?.Trim();
        e.SmtpPort = Input.SmtpPort;
        e.SmtpUser = Input.SmtpUser?.Trim();
        e.SmtpPassword = Input.SmtpPassword;

        await _db.SaveChangesAsync();

        TempData["Ok"] = (Input.Id > 0 ? "Güncellendi." : "Kaydedildi.");

        // id'yi taşımamak için explicit redirect (formun temiz gelmesi için kritik)
        return RedirectToPage("./SirketTanimlari");
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var e = await _db.CompanySettings.FindAsync(id);
        if (e != null)
        {
            _db.CompanySettings.Remove(e);
            await _db.SaveChangesAsync();
            TempData["Ok"] = "Kayıt silindi.";
        }

        // id'yi taşımamak için explicit redirect
        return RedirectToPage("./SirketTanimlari");
    }
}
