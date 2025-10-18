window.PedidoPage = (function(){
  const containerId = 'pedidoTableContainer';

  function withSpinner(btn, running){
    if(!btn) return;
    if(running){
      btn.dataset.originalHtml = btn.innerHTML;
      btn.disabled = true;
      const text = btn.textContent ? btn.textContent.trim() : '';
      btn.innerHTML = `<span class="spinner-border spinner-border-sm me-1" role="status" aria-hidden="true"></span>${text}`;
    } else {
      btn.disabled = false;
      if(btn.dataset.originalHtml) btn.innerHTML = btn.dataset.originalHtml;
    }
  }

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

    c.querySelectorAll('[data-action="view"]').forEach(btn => {
      btn.addEventListener('click', () => Ui.openModalFromUrl(`/Pedido/Visualizar?id=${btn.dataset.id}`, I18n.t('toast.ok')));
    });

    c.querySelectorAll('[data-action="confirm"]').forEach(btn => {
      btn.addEventListener('click', () => {
        Ui.confirm({
          title: I18n.t('confirm.order.title'),
          message: I18n.t('confirm.order.msg'),
          style: 'success',
          confirmText: I18n.t('toast.ok'),
          onConfirm: async () => {
            const ok = await Ui.postJson(`/Pedido/Confirmar?id=${btn.dataset.id}`, null, 'POST');
            Ui.toast({ style: ok? 'success':'danger', title: I18n.t(ok? 'toast.success':'toast.error'), message: I18n.t(ok? 'msg.order.confirmed':'msg.order.confirmFailed')});
            await reloadTable();
          }
        });
      });
    });

    c.querySelectorAll('[data-action="cancel"]').forEach(btn => {
      btn.addEventListener('click', () => {
        Ui.confirm({
          title: I18n.t('confirm.cancel.title'),
          message: I18n.t('confirm.cancel.msg'),
          style: 'warning',
          confirmText: I18n.t('toast.ok'),
          onConfirm: async () => {
            const ok = await Ui.postJson(`/Pedido/Cancelar?id=${btn.dataset.id}`, null, 'POST');
            Ui.toast({ style: ok? 'success':'danger', title: I18n.t(ok? 'toast.success':'toast.error'), message: I18n.t(ok? 'msg.order.canceled':'msg.order.cancelFailed')});
            await reloadTable();
          }
        });
      });
    });

    c.querySelectorAll('[data-action="invoice"]').forEach(btn => {
      btn.addEventListener('click', async () => {
        withSpinner(btn, true);
        const ok = await Ui.postJson(`/Pedido/Faturar?pedidoId=${btn.dataset.id}`, null, 'POST');
        withSpinner(btn, false);
        Ui.toast({ style: ok? 'success':'danger', title: I18n.t('toast.invoiceTitle'), message: I18n.t(ok? 'msg.invoice.success':'msg.invoice.fail')});
        await reloadTable();
      });
    });

    c.querySelectorAll('[data-action="invoice-view"]').forEach(btn => {
      btn.addEventListener('click', async () => {
        const el = await Ui.openModalFromUrl(`/Pedido/VisualizarFatura?pedidoId=${btn.dataset.id}`, I18n.t('toast.invoiceTitle'));
        if(!el){ Ui.toast({ style:'warning', title:I18n.t('toast.invoiceTitle'), message:I18n.t('msg.invoice.notfound')}); }
      });
    });

    c.querySelectorAll('[data-action="receipt"]').forEach(btn => {
      btn.addEventListener('click', async () => {
        withSpinner(btn, true);
        const ok = await Ui.postJson(`/Pedido/EmitirRecibo?pedidoId=${btn.dataset.id}`, null, 'POST');
        withSpinner(btn, false);
        Ui.toast({ style: ok? 'success':'danger', title: I18n.t('toast.receiptTitle'), message: I18n.t(ok? 'msg.receipt.success':'msg.receipt.fail')});
        await reloadTable();
      });
    });

    c.querySelectorAll('[data-action="receipt-view"]').forEach(btn => {
      btn.addEventListener('click', async () => {
        const el = await Ui.openModalFromUrl(`/Pedido/VisualizarRecibo?pedidoId=${btn.dataset.id}`, I18n.t('toast.receiptTitle'));
        if(!el){ Ui.toast({ style:'warning', title:I18n.t('toast.receiptTitle'), message:I18n.t('msg.receipt.notfound')}); }
      });
    });
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
