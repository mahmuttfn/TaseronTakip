(function(){
  const sidebar = document.getElementById('sidebar');
  const toggle = document.getElementById('menuToggle');
  const toggleMobile = document.getElementById('menuToggleMobile');
  const state = localStorage.getItem('sidebar-collapsed');
  if(state === 'true'){ sidebar.classList.add('collapsed'); }
  if(toggle){
    toggle.addEventListener('click', () => {
      sidebar.classList.toggle('collapsed');
      localStorage.setItem('sidebar-collapsed', sidebar.classList.contains('collapsed'));
    });
  }
  if(toggleMobile){
    toggleMobile.addEventListener('click', () => {
      sidebar.classList.toggle('open');
    });
  }
  document.querySelectorAll('[data-toggle="submenu"]').forEach(btn => {
    btn.addEventListener('click', () => {
      btn.classList.toggle('open');
      const ul = btn.nextElementSibling; if(!ul) return; ul.classList.toggle('show');
    });
  });
})();