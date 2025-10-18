window.ProdutoPage = (function(){
  const containerId = 'produtoTableContainer';

  async function reloadTable(page){
    const filtro = document.getElementById('txtFiltroProduto').value.trim();
    const url = `/Produto/Tabela?filtro=${encodeURIComponent(filtro)}${page?`&page=${page}`:''}`;
    await Ui.loadTable({ url, containerId });
    wireRowActions();
  }

  function wireRowActions(){
    const c = document.getElementById(containerId);
    if(!c) return;
    c.querySelectorAll('[data-page]')?.forEach(a => a.addEventListener('click', ev => { ev.preventDefault(); reloadTable(a.getAttribute('data-page')); }));
    c.querySelectorAll('[data-action="view"]').forEach(btn => btn.addEventListener('click', () => Ui.openModalFromUrl(`/Produto/Visualizar?id=${btn.dataset.id}`, 'Detalhes do Produto')));
    c.querySelectorAll('[data-action="edit"]').forEach(btn => btn.addEventListener('click', () => Ui.openModalFromUrl(`/Produto/Editar?id=${btn.dataset.id}`, 'Editar Produto')));
    c.querySelectorAll('[data-action="delete"]').forEach(btn => btn.addEventListener('click', () => Ui.confirm({
      title: 'Excluir Produto',
      message: 'Tem certeza que deseja excluir este produto?',
      style: 'danger',
      confirmText: 'Excluir',
      onConfirm: async () => {
        const ok = await Ui.postJson(`/Produto/Excluir?id=${btn.dataset.id}`, null, 'POST');
        await reloadTable();
      }
    })));
  }

  function init(){
    document.getElementById('btnAddProduto').addEventListener('click', () => Ui.openModalFromUrl('/Produto/Criar', 'Novo Produto'));
    document.getElementById('btnFiltrarProduto').addEventListener('click', () => reloadTable());
    const txt = document.getElementById('txtFiltroProduto');
    txt.addEventListener('keypress', (e)=>{ if(e.key==='Enter'){ e.preventDefault(); reloadTable(); }});
    reloadTable();
  }

  return { init, reloadTable };
})();
