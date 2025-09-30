using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TaseronTakip.Models;

namespace TaseronTakip.Pages
{
    public class EvrakTurleriKurallarModel : PageModel
    {
        private readonly AppDbContext _db;

        public EvrakTurleriKurallarModel(AppDbContext db)
        {
            _db = db;
        }

        public IList<EvrakTurKural> Kayitlar { get; set; } = new List<EvrakTurKural>();

        [BindProperty]
        public EvrakTurInput Input { get; set; } = new();

        public void OnGet(int? id)
        {
            Kayitlar = _db.EvrakTurKurallari
                          .OrderBy(x => x.Kategori)
                          .ThenBy(x => x.Ad)
                          .ToList();

            if (id.HasValue)
            {
                var ent = _db.EvrakTurKurallari.Find(id.Value);
                if (ent != null) Input = EvrakTurInput.FromEntity(ent);
            }
        }

        public IActionResult OnPostCreate()
        {
            Kayitlar = _db.EvrakTurKurallari.OrderBy(x => x.Kategori).ThenBy(x => x.Ad).ToList();
            if (!ModelState.IsValid) return Page();

            var entity = Input.ToEntity();
            _db.EvrakTurKurallari.Add(entity);
            _db.SaveChanges();
            return RedirectToPage();
        }

        public IActionResult OnPostEdit()
        {
            Kayitlar = _db.EvrakTurKurallari.OrderBy(x => x.Kategori).ThenBy(x => x.Ad).ToList();
            if (!ModelState.IsValid) return Page();

            var ent = _db.EvrakTurKurallari.Find(Input.Id);
            if (ent == null) return NotFound();

            Input.ApplyTo(ent);
            ent.UpdatedAt = DateTime.UtcNow;
            _db.SaveChanges();
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var ent = _db.EvrakTurKurallari.Find(id);
            if (ent == null) return NotFound();

            _db.EvrakTurKurallari.Remove(ent);
            _db.SaveChanges();
            return RedirectToPage();
        }

        public IActionResult OnPostToggle(int id)
        {
            var ent = _db.EvrakTurKurallari.Find(id);
            if (ent == null) return NotFound();

            ent.Pasif = !ent.Pasif;
            ent.UpdatedAt = DateTime.UtcNow;
            _db.SaveChanges();
            return RedirectToPage();
        }

        // --- ViewModel ---
        public class EvrakTurInput
        {
            public int Id { get; set; }

            [Required, MaxLength(128)]
            public string Ad { get; set; } = string.Empty;

            [Required]
            public EvrakKategori Kategori { get; set; } = EvrakKategori.Personel;

            [Range(0, 1200)]
            public int GecerlilikSuresiDeger { get; set; } = 0;

            [Required]
            public SureBirim GecerlilikSuresiBirim { get; set; } = SureBirim.Ay;

            public bool BelgeUzerindekiTarihtenHesapla { get; set; } = true;

            [Range(0, 1200)]
            public int KaynakBelgeTazelikDeger { get; set; } = 0;
            public SureBirim KaynakBelgeTazelikBirim { get; set; } = SureBirim.Ay;
            public bool TazelikKuraliniZorunluKil { get; set; } = false;

            public bool ZorunluMu { get; set; } = true;
            [Range(0, 3650)]
            public int HatirlatmaGunOnce { get; set; } = 7;

            [MaxLength(256)]
            public string? Aciklama { get; set; }
            [MaxLength(128)]
            public string? KabulEdilenUzantilarCsv { get; set; } = "pdf,jpg,png";
            [Range(0, 10240)]
            public int? MaxDosyaBoyutuMb { get; set; } = 10;
            [Range(1, 10)]
            public int MinDosyaAdedi { get; set; } = 1;

            public bool TekilBelgeMi { get; set; } = true;
            public bool EDevlettenAlinmaliMi { get; set; } = false;
            public bool BarkodQrZorunluMu { get; set; } = false;
            public bool PlakaZorunluMu { get; set; } = false;
            public bool TCKNZorunluMu { get; set; } = false;

            public bool Pasif { get; set; } = false;

            // helpers
            public EvrakTurKural ToEntity()
            {
                return new EvrakTurKural
                {
                    Ad = Ad.Trim(),
                    Kategori = Kategori,
                    GecerlilikSuresiDeger = GecerlilikSuresiDeger,
                    GecerlilikSuresiBirim = GecerlilikSuresiBirim,
                    BelgeUzerindekiTarihtenHesapla = BelgeUzerindekiTarihtenHesapla,
                    KaynakBelgeTazelikDeger = KaynakBelgeTazelikDeger,
                    KaynakBelgeTazelikBirim = KaynakBelgeTazelikBirim,
                    TazelikKuraliniZorunluKil = TazelikKuraliniZorunluKil,
                    ZorunluMu = ZorunluMu,
                    HatirlatmaGunOnce = HatirlatmaGunOnce,
                    Aciklama = Aciklama,
                    KabulEdilenUzantilarCsv = KabulEdilenUzantilarCsv,
                    MaxDosyaBoyutuMb = MaxDosyaBoyutuMb,
                    MinDosyaAdedi = MinDosyaAdedi,
                    TekilBelgeMi = TekilBelgeMi,
                    EDevlettenAlinmaliMi = EDevlettenAlinmaliMi,
                    BarkodQrZorunluMu = BarkodQrZorunluMu,
                    PlakaZorunluMu = PlakaZorunluMu,
                    TCKNZorunluMu = TCKNZorunluMu,
                    Pasif = Pasif
                };
            }

            public static EvrakTurInput FromEntity(EvrakTurKural e)
            {
                return new EvrakTurInput
                {
                    Id = e.Id,
                    Ad = e.Ad,
                    Kategori = e.Kategori,
                    GecerlilikSuresiDeger = e.GecerlilikSuresiDeger,
                    GecerlilikSuresiBirim = e.GecerlilikSuresiBirim,
                    BelgeUzerindekiTarihtenHesapla = e.BelgeUzerindekiTarihtenHesapla,
                    KaynakBelgeTazelikDeger = e.KaynakBelgeTazelikDeger,
                    KaynakBelgeTazelikBirim = e.KaynakBelgeTazelikBirim,
                    TazelikKuraliniZorunluKil = e.TazelikKuraliniZorunluKil,
                    ZorunluMu = e.ZorunluMu,
                    HatirlatmaGunOnce = e.HatirlatmaGunOnce,
                    Aciklama = e.Aciklama,
                    KabulEdilenUzantilarCsv = e.KabulEdilenUzantilarCsv,
                    MaxDosyaBoyutuMb = e.MaxDosyaBoyutuMb,
                    MinDosyaAdedi = e.MinDosyaAdedi,
                    TekilBelgeMi = e.TekilBelgeMi,
                    EDevlettenAlinmaliMi = e.EDevlettenAlinmaliMi,
                    BarkodQrZorunluMu = e.BarkodQrZorunluMu,
                    PlakaZorunluMu = e.PlakaZorunluMu,
                    TCKNZorunluMu = e.TCKNZorunluMu,
                    Pasif = e.Pasif
                };
            }

            public void ApplyTo(EvrakTurKural e)
            {
                e.Ad = Ad.Trim();
                e.Kategori = Kategori;
                e.GecerlilikSuresiDeger = GecerlilikSuresiDeger;
                e.GecerlilikSuresiBirim = GecerlilikSuresiBirim;
                e.BelgeUzerindekiTarihtenHesapla = BelgeUzerindekiTarihtenHesapla;
                e.KaynakBelgeTazelikDeger = KaynakBelgeTazelikDeger;
                e.KaynakBelgeTazelikBirim = KaynakBelgeTazelikBirim;
                e.TazelikKuraliniZorunluKil = TazelikKuraliniZorunluKil;
                e.ZorunluMu = ZorunluMu;
                e.HatirlatmaGunOnce = HatirlatmaGunOnce;
                e.Aciklama = Aciklama;
                e.KabulEdilenUzantilarCsv = KabulEdilenUzantilarCsv;
                e.MaxDosyaBoyutuMb = MaxDosyaBoyutuMb;
                e.MinDosyaAdedi = MinDosyaAdedi;
                e.TekilBelgeMi = TekilBelgeMi;
                e.EDevlettenAlinmaliMi = EDevlettenAlinmaliMi;
                e.BarkodQrZorunluMu = BarkodQrZorunluMu;
                e.PlakaZorunluMu = PlakaZorunluMu;
                e.TCKNZorunluMu = TCKNZorunluMu;
                e.Pasif = Pasif;
            }
        }
    }
}
