using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaseronTakip.Models;

namespace TaseronTakip.Pages;

public class RandevuTalepModel : PageModel
{
    private readonly AppDbContext _db;
    public RandevuTalepModel(AppDbContext db) => _db = db;

    public List<string> Firmalar { get; set; } = new() { "ACME", "Globex", "Initech" };
    public List<string> EvrakTurleri { get; set; } = new() { "Ziyaretçi Kartı", "Güvenlik Eğitimi", "KVKK Formu" };
    public List<string> Bolumler { get; set; } = new() { "Üretim", "Bakım", "Satınalma", "İK" };

    [BindProperty] public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required] public string? Firma { get; set; }
        [Required, Display(Name = "İlgili Kişi")] public string? IlgiliKisi { get; set; }
        [Required, EmailAddress] public string? Email { get; set; }
        public string? IletisimNo { get; set; }
        [DataType(DataType.Date)] public DateTime? ZiyaretTarihi { get; set; }
        [DataType(DataType.Time)] public TimeSpan? ZiyaretSaati { get; set; }
        public string? Aciklama { get; set; }

        public List<DocRow> Docs { get; set; } = new();
        public List<StaffRow> Staff { get; set; } = new();
    }
    public class DocRow { public string? Tip { get; set; } public string? Not { get; set; } }
    public class StaffRow { public string? Bolum { get; set; } public string? Personel { get; set; } }

    public void OnGet() { }

    public async Task<IActionResult> OnPostSendAsync()
    {
        if (!ModelState.IsValid) return Page();

        var req = new AppointmentRequest
        {
            Firma = Input.Firma!.Trim(),
            IlgiliKisi = Input.IlgiliKisi!.Trim(),
            Email = Input.Email!.Trim(),
            IletisimNo = Input.IletisimNo?.Trim(),
            ZiyaretTarihi = Input.ZiyaretTarihi,
            ZiyaretSaati = Input.ZiyaretSaati,
            Aciklama = Input.Aciklama,
            CreatedAt = DateTime.UtcNow
        };
        _db.AppointmentRequests.Add(req);
        await _db.SaveChangesAsync();

        foreach (var d in Input.Docs)
            _db.RequestedDocuments.Add(new RequestedDocument { AppointmentRequestId = req.Id, Tip = d.Tip, Not = d.Not });

        foreach (var s in Input.Staff)
            _db.DepartmentStaff.Add(new DepartmentStaff { AppointmentRequestId = req.Id, Bolum = s.Bolum, Personel = s.Personel });

        await _db.SaveChangesAsync();

        TempData["Ok"] = "Davet gönderildi.";
        return RedirectToPage("./RandevuTalep"); // PRG
    }
}
