window.PedidoPage = (function(){
  const containerId = 'pedidoTableContainer';

  async function reloadTable(page){
    const filtro = document.getElementById('txtFiltroPedido').value.trim();
    const url = `/Pedido/Tabela?filtro=${encodeURIComponent(filtro)}${page?`&page=${page}`:''}`;
    await Ui.loadTable({ url, containerId });
    wireRowActions();
  }

  function wireRowActions(){
    const c = document.getElementById(containerId);
    if(!c) return;
    c.querySelectorAll('[data-page]')?.forEach(a => a.addEventListener('click', ev => { ev.preventDefault(); reloadTable(a.getAttribute('data-page')); }));
    c.querySelectorAll('[data-action="view"]').forEach(btn => btn.addEventListener('click', () => Ui.openModalFromUrl(`/Pedido/Visualizar?id=${btn.dataset.id}`, 'Detalhes do Pedido')));
    c.querySelectorAll('[data-action="confirm"]').forEach(btn => btn.addEventListener('click', () => Ui.confirm({
      title: 'Confirmar Pedido', message: 'Deseja confirmar o pedido?', style:'success', confirmText:'Confirmar',
      onConfirm: async ()=>{ await Ui.postJson(`/Pedido/Confirmar?id=${btn.dataset.id}`, null, 'POST'); await reloadTable(); }
    })));
    c.querySelectorAll('[data-action="cancel"]').forEach(btn => btn.addEventListener('click', () => Ui.confirm({
      title: 'Cancelar Pedido', message: 'Deseja cancelar o pedido?', style:'warning', confirmText:'Cancelar',
      onConfirm: async ()=>{ await Ui.postJson(`/Pedido/Cancelar?id=${btn.dataset.id}`, null, 'POST'); await reloadTable(); }
    })));
  }

  function init(){
    document.getElementById('btnAddPedido').addEventListener('click', () => Ui.openModalFromUrl('/Pedido/Criar', 'Novo Pedido'));
    document.getElementById('btnFiltrarPedido').addEventListener('click', () => reloadTable());
    const txt = document.getElementById('txtFiltroPedido');
    txt.addEventListener('keypress', (e)=>{ if(e.key==='Enter'){ e.preventDefault(); reloadTable(); }});
    reloadTable();
  }

  return { init, reloadTable };
})();
