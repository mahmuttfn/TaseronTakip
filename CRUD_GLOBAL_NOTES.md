# Global CRUD güncellemeleri eklendi
- Pages/Shared/_ToastBar.cshtml (yeşil, geniş toast)
- Pages/Shared/_ValidationScriptsPartial.cshtml (CDN doğrulama)
- wwwroot/js/app-common.js (Save/Delete onayı, form temizleme, PRG benzeri URL düzeltme)
- Layout(lar): @_ToastBar ve app-common.js bağlandı

Kullanım:
- Kaydet/Güncelle formu: asp-page-handler="Save"
- Sil formu: asp-page-handler="Delete" + asp-route-id
- Temizle butonu: id="btnClear" veya class="js-clear-form"
- Başarılı işlem: TempData["Ok"]="Mesaj"; return RedirectToPage("./Sayfa");
