window.EstoquePage = (function(){
  const containerId = 'estoqueTableContainer';

  let debounce;
  function scheduleReload(){
    clearTimeout(debounce);
    const txt = document.getElementById('txtFiltroEstoque');
    const v = (txt.value||'').trim();
    if(v.length === 0){ debounce = setTimeout(()=> reloadTable(1), 200); return; }
    if(v.length >= 3){ debounce = setTimeout(()=> reloadTable(1), 300); }
  }

  async function reloadTable(page){
    const filtro = document.getElementById('txtFiltroEstoque').value.trim();
    const url = `/Estoque/Tabela?filtro=${encodeURIComponent(filtro)}${page?`&page=${page}`:''}`;
    await Ui.loadTable({ url, containerId });
    wireRowActions();
  }

  function wireRowActions(){
    const c = document.getElementById(containerId);
    if(!c) return;
    c.querySelectorAll('[data-page]')?.forEach(a => a.addEventListener('click', ev => { ev.preventDefault(); reloadTable(a.getAttribute('data-page')); }));
    c.querySelectorAll('[data-action="view"]').forEach(btn => btn.addEventListener('click', () => Ui.openModalFromUrl(`/Estoque/Visualizar?produtoId=${btn.dataset.id}`, 'Estoque do Produto')));
    c.querySelectorAll('[data-action="entrada"]').forEach(btn => btn.addEventListener('click', async () => { window.__movAction='entrada'; Ui.openModalFromUrl(`/Estoque/MovimentoEntrada?produtoId=${btn.dataset.id}`, 'Entrada de Estoque'); }));
    c.querySelectorAll('[data-action="saida"]').forEach(btn => btn.addEventListener('click', async () => { window.__movAction='saida'; Ui.openModalFromUrl(`/Estoque/MovimentoSaida?produtoId=${btn.dataset.id}`, 'Sa�da de Estoque'); }));
  }

  function init(){
    document.getElementById('btnMovEntrada').addEventListener('click', () => { window.__movAction='entrada'; Ui.openModalFromUrl('/Estoque/MovimentoEntrada', 'Entrada de Estoque'); });
    document.getElementById('btnMovSaida').addEventListener('click', () => { window.__movAction='saida'; Ui.openModalFromUrl('/Estoque/MovimentoSaida', 'Sa�da de Estoque'); });
    document.getElementById('btnFiltrarEstoque').addEventListener('click', () => reloadTable());
    const txt = document.getElementById('txtFiltroEstoque');
    txt.addEventListener('input', scheduleReload);
    txt.addEventListener('keypress', (e)=>{ if(e.key==='Enter'){ e.preventDefault(); reloadTable(); }});
    reloadTable();
  }

  return { init, reloadTable };
})();
