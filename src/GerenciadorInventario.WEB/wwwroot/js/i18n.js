(function(){
  const dictionaries = {
    'pt-BR': {
      'toast.ok': 'Ok',
      'toast.error': 'Erro',
      'toast.success': 'Sucesso',
      'toast.invoiceTitle': 'Faturamento',
      'toast.receiptTitle': 'Recibo',
      'confirm.order.title': 'Confirmar Pedido',
      'confirm.order.msg': 'Deseja confirmar o pedido?',
      'confirm.cancel.title': 'Cancelar Pedido',
      'confirm.cancel.msg': 'Deseja cancelar o pedido?',
      'msg.order.confirmed': 'Pedido confirmado.',
      'msg.order.confirmFailed': 'Falha ao confirmar.',
      'msg.order.canceled': 'Pedido cancelado.',
      'msg.order.cancelFailed': 'Falha ao cancelar.',
      'msg.invoice.success': 'Pedido faturado com sucesso.',
      'msg.invoice.fail': 'Falha ao faturar o pedido.',
      'msg.invoice.notfound': 'Fatura não encontrada.',
      'msg.receipt.success': 'Recibo emitido com sucesso.',
      'msg.receipt.fail': 'Falha ao emitir recibo.',
      'msg.receipt.notfound': 'Recibo não encontrado.'
    },
    'en-US': {
      'toast.ok': 'Ok',
      'toast.error': 'Error',
      'toast.success': 'Success',
      'toast.invoiceTitle': 'Invoice',
      'toast.receiptTitle': 'Receipt',
      'confirm.order.title': 'Confirm Order',
      'confirm.order.msg': 'Do you want to confirm the order?',
      'confirm.cancel.title': 'Cancel Order',
      'confirm.cancel.msg': 'Do you want to cancel the order?',
      'msg.order.confirmed': 'Order confirmed.',
      'msg.order.confirmFailed': 'Failed to confirm.',
      'msg.order.canceled': 'Order canceled.',
      'msg.order.cancelFailed': 'Failed to cancel.',
      'msg.invoice.success': 'Order invoiced successfully.',
      'msg.invoice.fail': 'Failed to invoice order.',
      'msg.invoice.notfound': 'Invoice not found.',
      'msg.receipt.success': 'Receipt issued successfully.',
      'msg.receipt.fail': 'Failed to issue receipt.',
      'msg.receipt.notfound': 'Receipt not found.'
    }
  };

  let current = 'pt-BR';
  function setLocale(loc){ if(dictionaries[loc]) current = loc; }
  function t(key){ return (dictionaries[current] && dictionaries[current][key]) || (dictionaries['pt-BR'][key]) || key; }

  window.I18n = { setLocale, t };
})();
