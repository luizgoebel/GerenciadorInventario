window.ReciboPage = (function(){
  const containerId = 'reciboTableContainer';

  async function reloadTable(page){
    const filtro = document.getElementById('txtFiltroRecibo').value.trim();
    const faturaId = filtro ? parseInt(filtro) : '';
    const url = `/Recibo/Tabela?${faturaId?`faturaId=${faturaId}&`:''}${page?`page=${page}`:''}`;
    await Ui.loadTable({ url, containerId });
    wireRowActions();
  }

  function wireRowActions(){
    const c = document.getElementById(containerId);
    if(!c) return;
    c.querySelectorAll('[data-page]')?.forEach(a => a.addEventListener('click', ev => { ev.preventDefault(); reloadTable(a.getAttribute('data-page')); }));
    c.querySelectorAll('[data-action="view"]').forEach(btn => btn.addEventListener('click', () => Ui.openModalFromUrl(`/Recibo/Visualizar?id=${btn.dataset.id}`, 'Detalhes do Recibo')));
  }

  function init(){
    document.getElementById('btnFiltrarRecibo').addEventListener('click', () => reloadTable());
    const txt = document.getElementById('txtFiltroRecibo');
    txt.addEventListener('keypress', (e)=>{ if(e.key==='Enter'){ e.preventDefault(); reloadTable(); }});
    reloadTable();
  }

  return { init, reloadTable };
})();
