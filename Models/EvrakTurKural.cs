using System;
using System.ComponentModel.DataAnnotations;

namespace TaseronTakip.Models
{
    /// <summary>
    /// Evrak türü bazlı kural tanımı.
    /// </summary>
    public class EvrakTurKural
    {
        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string Ad { get; set; } = string.Empty;

        [Required]
        public EvrakKategori Kategori { get; set; } = EvrakKategori.Personel;

        /// <summary>Belge geçerlilik süresi (0 = sınırsız / uygulanmaz)</summary>
        [Range(0, 1200)]
        public int GecerlilikSuresiDeger { get; set; } = 0;

        [Required]
        public SureBirim GecerlilikSuresiBirim { get; set; } = SureBirim.Ay;

        /// <summary>
        /// Geçerlilik hesabı belge üzerindeki tarihten mi (ör. rapor tarihi) yoksa onay/kabul tarihinden mi başlasın?
        /// </summary>
        public bool BelgeUzerindekiTarihtenHesapla { get; set; } = true;

        /// <summary>Belgenin kendisi son X (gün/ay/yıl) içinde alınmış olmalı mı? (0 = zorunlu değil)</summary>
        [Range(0, 1200)]
        public int KaynakBelgeTazelikDeger { get; set; } = 0;

        public SureBirim KaynakBelgeTazelikBirim { get; set; } = SureBirim.Ay;

        public bool TazelikKuraliniZorunluKil { get; set; } = false;

        public bool ZorunluMu { get; set; } = true;                    // giriş için zorunlu
        [Range(0, 3650)]
        public int HatirlatmaGunOnce { get; set; } = 7;               // bitmeden kaç gün önce uyar

        [MaxLength(256)]
        public string? Aciklama { get; set; }                         // tek satır not
        [MaxLength(128)]
        public string? KabulEdilenUzantilarCsv { get; set; } = "pdf,jpg,png";
        [Range(0, 10240)]
        public int? MaxDosyaBoyutuMb { get; set; } = 10;
        [Range(1, 10)]
        public int MinDosyaAdedi { get; set; } = 1;

        public bool TekilBelgeMi { get; set; } = true;                // kişi/araç başına 1 aktif belge
        public bool EDevlettenAlinmaliMi { get; set; } = false;       // e-Devlet zorunlu mu
        public bool BarkodQrZorunluMu { get; set; } = false;          // doğrulama için
        public bool PlakaZorunluMu { get; set; } = false;             // araç evraklarında
        public bool TCKNZorunluMu { get; set; } = false;              // personel evraklarında

        public bool Pasif { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
