using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TaseronTakip.Models;

namespace TaseronTakip.Pages
{
    public class SirketTanimlariModel : PageModel
    {
        private readonly AppDbContext _db;
        public SirketTanimlariModel(AppDbContext db) => _db = db;

        public IList<Sirket> Kayitlar { get; set; } = new List<Sirket>();

        [BindProperty]
        public SirketInput Input { get; set; } = new();

        public void OnGet(int? id)
        {
            Kayitlar = _db.Set<Sirket>().OrderBy(x => x.Unvan).ToList();

            if (id.HasValue)
            {
                var ent = _db.Set<Sirket>().Find(id.Value);
                if (ent != null) Input = SirketInput.FromEntity(ent);
            }
        }

        public IActionResult OnPostCreate()
        {
            Kayitlar = _db.Set<Sirket>().OrderBy(x => x.Unvan).ToList();
            if (!ModelState.IsValid) return Page();

            var ent = Input.ToEntity();
            _db.Add(ent);
            _db.SaveChanges();
            return RedirectToPage();
        }

        public IActionResult OnPostEdit()
        {
            Kayitlar = _db.Set<Sirket>().OrderBy(x => x.Unvan).ToList();
            if (!ModelState.IsValid) return Page();

            var ent = _db.Set<Sirket>().Find(Input.Id);
            if (ent == null) return NotFound();

            Input.ApplyTo(ent);
            _db.SaveChanges();
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var ent = _db.Set<Sirket>().Find(id);
            if (ent == null) return NotFound();
            _db.Remove(ent);
            _db.SaveChanges();
            return RedirectToPage();
        }

        public IActionResult OnPostToggle(int id)
        {
            var ent = _db.Set<Sirket>().Find(id);
            if (ent == null) return NotFound();
            ent.Aktif = !ent.Aktif;
            _db.SaveChanges();
            return RedirectToPage();
        }

        public class SirketInput
        {
            public int Id { get; set; }

            [Required, MaxLength(160)]
            public string Unvan { get; set; } = string.Empty;

            [MaxLength(40)] public string? VergiDairesi { get; set; }
            [MaxLength(20)] public string? VergiNo { get; set; }
            [MaxLength(30)] public string? MersisNo { get; set; }
            [MaxLength(30)] public string? TicaretSicilNo { get; set; }
            [MaxLength(40)] public string? SGKIşyeriSicilNo { get; set; }
            [MaxLength(120)] public string? KEPAdres { get; set; }
            [MaxLength(120)] public string? WebSite { get; set; }
            [MaxLength(20)] public string? Telefon { get; set; }
            [MaxLength(120)] public string? YetkiliAdSoyad { get; set; }
            [MaxLength(60)] public string? YetkiliGorev { get; set; }
            [MaxLength(20)] public string? YetkiliTelefon { get; set; }
            [MaxLength(34)] public string? IBAN { get; set; }
            [MaxLength(80)] public string? BankaAdi { get; set; }
            [MaxLength(240)] public string? Adres { get; set; }
            public bool Aktif { get; set; } = true;

            public Sirket ToEntity() => new()
            {
                Unvan = Unvan.Trim(),
                VergiDairesi = VergiDairesi,
                VergiNo = VergiNo,
                MersisNo = MersisNo,
                TicaretSicilNo = TicaretSicilNo,
                SGKIşyeriSicilNo = SGKIşyeriSicilNo,
                KEPAdres = KEPAdres,
                WebSite = WebSite,
                Telefon = Telefon,
                YetkiliAdSoyad = YetkiliAdSoyad,
                YetkiliGorev = YetkiliGorev,
                YetkiliTelefon = YetkiliTelefon,
                IBAN = IBAN,
                BankaAdi = BankaAdi,
                Adres = Adres,
                Aktif = Aktif
            };

            public static SirketInput FromEntity(Sirket e) => new()
            {
                Id = e.Id,
                Unvan = e.Unvan,
                VergiDairesi = e.VergiDairesi,
                VergiNo = e.VergiNo,
                MersisNo = e.MersisNo,
                TicaretSicilNo = e.TicaretSicilNo,
                SGKIşyeriSicilNo = e.SGKIşyeriSicilNo,
                KEPAdres = e.KEPAdres,
                WebSite = e.WebSite,
                Telefon = e.Telefon,
                YetkiliAdSoyad = e.YetkiliAdSoyad,
                YetkiliGorev = e.YetkiliGorev,
                YetkiliTelefon = e.YetkiliTelefon,
                IBAN = e.IBAN,
                BankaAdi = e.BankaAdi,
                Adres = e.Adres,
                Aktif = e.Aktif
            };

            public void ApplyTo(Sirket e)
            {
                e.Unvan = Unvan.Trim();
                e.VergiDairesi = VergiDairesi; e.VergiNo = VergiNo;
                e.MersisNo = MersisNo; e.TicaretSicilNo = TicaretSicilNo;
                e.SGKIşyeriSicilNo = SGKIşyeriSicilNo; e.KEPAdres = KEPAdres;
                e.WebSite = WebSite; e.Telefon = Telefon;
                e.YetkiliAdSoyad = YetkiliAdSoyad; e.YetkiliGorev = YetkiliGorev; e.YetkiliTelefon = YetkiliTelefon;
                e.IBAN = IBAN; e.BankaAdi = BankaAdi;
                e.Adres = Adres; e.Aktif = Aktif;
            }
        }
    }
}
