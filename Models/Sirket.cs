using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaseronTakip.Models
{
    [Table("Sirketler")]
    public class Sirket
    {
        public int Id { get; set; }

        [Required, MaxLength(160)]
        public string Unvan { get; set; } = string.Empty;

        [MaxLength(40)]
        public string? VergiDairesi { get; set; }

        [MaxLength(20)]
        public string? VergiNo { get; set; }

        [MaxLength(30)]
        public string? MersisNo { get; set; }

        [MaxLength(30)]
        public string? TicaretSicilNo { get; set; }

        [MaxLength(40)]
        public string? SGKIşyeriSicilNo { get; set; }

        [MaxLength(120)]
        public string? KEPAdres { get; set; }

        [MaxLength(120), Url]
        public string? WebSite { get; set; }

        [MaxLength(20)]
        public string? Telefon { get; set; }

        [MaxLength(120)]
        public string? YetkiliAdSoyad { get; set; }

        [MaxLength(60)]
        public string? YetkiliGorev { get; set; }

        [MaxLength(20)]
        public string? YetkiliTelefon { get; set; }

        [MaxLength(34)]
        public string? IBAN { get; set; }

        [MaxLength(80)]
        public string? BankaAdi { get; set; }

        [MaxLength(240)]
        public string? Adres { get; set; }

        public bool Aktif { get; set; } = true;

        // NOT: Eski alanlar KALDIRILDI: Mail, TaseronDosyaNo / TaseronDosyaYoluNo vb.
    }
}
