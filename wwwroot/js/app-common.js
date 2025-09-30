\
(function(){
  // ---- Toast ----
  const toast=document.getElementById('toast'); 
  const initial=toast?.dataset?.msg ? String(toast.dataset.msg).trim() : "";
  function showToast(msg,ms=4000){
    if(!toast) return; toast.textContent=msg; toast.classList.add('show');
    setTimeout(()=>toast.classList.remove('show'),ms);
  }
  if(initial) showToast(initial);

  // ---- Save/Delete onayı (action'daki handler'a göre) ----
  const forms=Array.from(document.querySelectorAll('form'));
  forms.forEach(f=>{
    const act=(f.getAttribute('action')||"")+window.location.search;
    const isSave=/handler=Save/i.test(act);
    const isDelete=/handler=Delete/i.test(act);
    const custom=f.getAttribute('data-confirm');
    if(custom){ f.addEventListener('submit',e=>{ if(!window.confirm(custom)) e.preventDefault(); }); return; }
    if(isDelete){ f.addEventListener('submit',e=>{ if(!window.confirm('Kayıt silinsin mi?')) e.preventDefault(); }); }
    else if(isSave){
      let isUpdate=false;
      const hid=f.querySelector('input[type=hidden][id$="Input_Id"],input[name$=".Id"],input[name="Id"]');
      if(hid){ const v=parseInt(hid.value||"0",10); if(!Number.isNaN(v)&&v>0) isUpdate=true; }
      f.addEventListener('submit',e=>{ if(!window.confirm(isUpdate?'Bilgiler güncellensin mi?':'Bilgiler kaydedilsin mi?')) e.preventDefault(); });
    }
  });

  // ---- Form temizleme (global) ----
  function hardClearForm(container){
    const frm=container.closest('form')||document.querySelector('form'); if(!frm) return;
    frm.querySelectorAll('input,select,textarea').forEach(el=>{
      const id=(el.id||'').toLowerCase();
      if(el.type==='hidden'&&(id.endsWith('input_id')||el.name==='Id')){ el.value=0; return; }
      if(el.type==='checkbox'||el.type==='radio'){ el.checked=false; return; }
      el.value='';
    });
    showToast('Form temizlendi');
    // URL'den handler/id temizle
    try{
      const url=new URL(window.location.href);
      url.searchParams.delete('handler'); url.searchParams.delete('id');
      const m=url.pathname.match(/\/\d+$/); if(m){ url.pathname=url.pathname.replace(/\/\d+$/,''); }
      window.history.replaceState({},'',url.toString());
    }catch{}
  }
  document.addEventListener('click',function(e){
    const t=e.target; if(!(t instanceof HTMLElement)) return;
    if(t.matches('.js-clear-form,[data-action="clear-form"],#btnClear')){ e.preventDefault(); hardClearForm(t); }
  });

  // ---- ?handler=... temizle (PRG benzeri) ----
  try{
    const url=new URL(window.location.href);
    if(url.searchParams.has('handler')){ url.searchParams.delete('handler'); window.history.replaceState({},'',url.toString()); }
  }catch{}
})();
