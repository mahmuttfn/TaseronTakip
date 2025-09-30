using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaseronTakip.Models;

namespace TaseronTakip.Pages;

public class LogKayitlariModel : PageModel
{
    private readonly AppDbContext _db;
    public LogKayitlariModel(AppDbContext db) => _db = db;

    public List<LogEntry> Rows { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public LogFilter Filter { get; set; } = new();

    public class LogFilter
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string? Kullanici { get; set; }
        public string? Eylem { get; set; }
    }

    public async Task OnGetAsync()
    {
        var q = _db.Loglar.AsNoTracking().AsQueryable();

        if (Filter.Start is DateTime s)
            q = q.Where(x => x.Zaman >= s.Date);

        if (Filter.End is DateTime e)
            q = q.Where(x => x.Zaman < e.Date.AddDays(1));

        if (!string.IsNullOrWhiteSpace(Filter.Kullanici))
        {
            var k = Filter.Kullanici.Trim().ToLower();
            q = q.Where(x => (x.Kullanici ?? "").ToLower().Contains(k));
        }

        if (!string.IsNullOrWhiteSpace(Filter.Eylem))
            q = q.Where(x => x.Eylem == Filter.Eylem);

        Rows = await q.OrderByDescending(x => x.Zaman).Take(1000).ToListAsync();
    }
}
