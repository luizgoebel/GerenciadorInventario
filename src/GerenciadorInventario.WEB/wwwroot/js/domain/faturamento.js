window.FaturamentoPage = (function(){
  const containerId = 'faturamentoTableContainer';

  async function reloadTable(page){
    const filtro = document.getElementById('txtFiltroFaturamento').value.trim();
    const pedidoId = filtro ? parseInt(filtro) : '';
    const url = `/Faturamento/Tabela?${pedidoId?`pedidoId=${pedidoId}&`:''}${page?`page=${page}`:''}`;
    await Ui.loadTable({ url, containerId });
    wireRowActions();
  }

  function wireRowActions(){
    const c = document.getElementById(containerId);
    if(!c) return;
    c.querySelectorAll('[data-page]')?.forEach(a => a.addEventListener('click', ev => { ev.preventDefault(); reloadTable(a.getAttribute('data-page')); }));
    c.querySelectorAll('[data-action="view"]').forEach(btn => btn.addEventListener('click', () => Ui.openModalFromUrl(`/Faturamento/Visualizar?id=${btn.dataset.id}`, 'Detalhes da Fatura')));
  }

  function init(){
    document.getElementById('btnFaturar').addEventListener('click', async ()=>{
      const pid = parseInt(document.getElementById('txtPedidoIdFaturar').value||0);
      if(!pid) return;
      await Ui.postJson(`/Faturamento/Faturar?pedidoId=${pid}`, null, 'POST');
      await reloadTable();
    });
    document.getElementById('btnFiltrarFaturamento').addEventListener('click', () => reloadTable());
    const txt = document.getElementById('txtFiltroFaturamento');
    txt.addEventListener('keypress', (e)=>{ if(e.key==='Enter'){ e.preventDefault(); reloadTable(); }});
    reloadTable();
  }

  return { init, reloadTable };
})();
