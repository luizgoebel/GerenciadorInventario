window.EstoquePage = (function(){
  const containerId = 'estoqueTableContainer';

  async function reloadTable(page){
    const filtro = document.getElementById('txtFiltroEstoque').value.trim();
    const produtoId = filtro ? parseInt(filtro) : '';
    const url = `/Estoque/Tabela?${produtoId?`produtoId=${produtoId}&`:''}${page?`page=${page}`:''}`;
    await Ui.loadTable({ url, containerId });
    wireRowActions();
  }

  function wireRowActions(){
    const c = document.getElementById(containerId);
    if(!c) return;
    c.querySelectorAll('[data-page]')?.forEach(a => a.addEventListener('click', ev => { ev.preventDefault(); reloadTable(a.getAttribute('data-page')); }));
    c.querySelectorAll('[data-action="view"]').forEach(btn => btn.addEventListener('click', () => Ui.openModalFromUrl(`/Estoque/Visualizar?produtoId=${btn.dataset.id}`, 'Estoque do Produto')));
    c.querySelectorAll('[data-action="entrada"]').forEach(btn => btn.addEventListener('click', async () => { window.__movAction='entrada'; Ui.openModalFromUrl(`/Estoque/MovimentoEntrada?produtoId=${btn.dataset.id}`, 'Entrada de Estoque'); }));
    c.querySelectorAll('[data-action="saida"]').forEach(btn => btn.addEventListener('click', async () => { window.__movAction='saida'; Ui.openModalFromUrl(`/Estoque/MovimentoSaida?produtoId=${btn.dataset.id}`, 'Saída de Estoque'); }));
  }

  function init(){
    document.getElementById('btnMovEntrada').addEventListener('click', () => { window.__movAction='entrada'; Ui.openModalFromUrl('/Estoque/MovimentoEntrada', 'Entrada de Estoque'); });
    document.getElementById('btnMovSaida').addEventListener('click', () => { window.__movAction='saida'; Ui.openModalFromUrl('/Estoque/MovimentoSaida', 'Saída de Estoque'); });
    document.getElementById('btnFiltrarEstoque').addEventListener('click', () => reloadTable());
    const txt = document.getElementById('txtFiltroEstoque');
    txt.addEventListener('keypress', (e)=>{ if(e.key==='Enter'){ e.preventDefault(); reloadTable(); }});
    reloadTable();
  }

  return { init, reloadTable };
})();
