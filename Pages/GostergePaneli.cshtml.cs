using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TaseronTakip.Pages;

public class GostergePaneliModel : PageModel
{
    public DateTime Now { get; private set; } = DateTime.Now;

    public TodayBox Today { get; private set; } = new();
    public List<Row> Inside { get; private set; } = new();
    public List<Row> TodayExits { get; private set; } = new();
    public List<string> Alerts { get; private set; } = new();

    public class TodayBox
    {
        public int PlanliZiyaret { get; set; }
        public int OnayBekleyen { get; set; }
        public int SuresiYaklasanEvrak { get; set; }
    }

    public class Row
    {
        public string Firma { get; set; } = "";
        public string Kisi { get; set; } = "";
        public string Bolum { get; set; } = "";
        public DateTime Zaman { get; set; }
    }

    public void OnGet()
    {
        // --- DEMO VERÝLER (DB baðlayýnca burayý sorgularla doldur) ---
        Today = new TodayBox
        {
            PlanliZiyaret = 3,
            OnayBekleyen = 2,
            SuresiYaklasanEvrak = 1
        };

        Inside = new List<Row>
        {
            // örnek: yeni kayýt eklemek istersen buraya push et
            // new Row { Firma="ACME", Kisi="Ali Veli", Bolum="Bakým", Zaman=DateTime.Today.AddHours(9).AddMinutes(25) }
        };

        TodayExits = new List<Row>
        {
            // new Row { Firma="Globex", Kisi="Ayþe Demir", Bolum="Üretim", Zaman=DateTime.Today.AddHours(14).AddMinutes(10) }
        };

        Alerts = new List<string>
        {
            // "X firmasýnýn Z-34 kartý bugün 17:00'de doluyor.",
            // "Y bölümünde 2 ziyaret onay bekliyor."
        };
    }
}
