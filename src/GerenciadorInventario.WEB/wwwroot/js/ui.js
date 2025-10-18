window.Ui = (function () {
  const modalHost = document.getElementById('modalHost');
  const confirmHost = document.getElementById('confirmHost');

  function buildModal(id, title, bodyHtml, footerHtml) {
    const el = document.createElement('div');
    el.innerHTML = `
<div class="modal fade" id="${id}" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">${title ?? ''}</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">${bodyHtml ?? ''}</div>
      ${footerHtml ? `<div class="modal-footer">${footerHtml}</div>` : ''}
    </div>
  </div>
</div>`;
    return el.firstElementChild;
  }

  async function openModalFromUrl(url, title) {
    const html = await fetch(url, { headers: { 'X-Requested-With': 'XMLHttpRequest' } }).then(r => r.text());
    const id = `mdl_${Date.now()}`;
    const modalEl = buildModal(id, title, html);
    modalHost.innerHTML = '';
    modalHost.appendChild(modalEl);
    const modal = new bootstrap.Modal(modalEl);
    modal.show();
    return modalEl;
  }

  function confirm({ title = 'Confirmação', message = '', style = 'primary', confirmText = 'Confirmar', cancelText = 'Cancelar', onConfirm }) {
    const id = `confirm_${Date.now()}`;
    const footer = `
      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">${cancelText}</button>
      <button type="button" class="btn btn-${style}" id="${id}_ok">${confirmText}</button>`;
    const el = buildModal(id, title, `<div class="lead">${message}</div>`, footer);
    confirmHost.innerHTML = '';
    confirmHost.appendChild(el);
    const modal = new bootstrap.Modal(el);
    el.addEventListener('shown.bs.modal', () => {
      document.getElementById(`${id}_ok`)?.addEventListener('click', async () => {
        try { await onConfirm?.(); } finally { modal.hide(); }
      });
    });
    modal.show();
    return el;
  }

  async function postJson(url, data, method = 'POST') {
    const resp = await fetch(url, {
      method,
      headers: { 'Content-Type': 'application/json', 'X-Requested-With': 'XMLHttpRequest' },
      body: data ? JSON.stringify(data) : null
    });
    if (!resp.ok) return null;
    const ct = resp.headers.get('content-type') || '';
    if (ct.includes('application/json')) return await resp.json();
    return await resp.text();
  }

  async function loadTable({ url, containerId }) {
    const container = document.getElementById(containerId);
    if (!container) return;
    container.innerHTML = `<div class="text-center py-4"><div class="spinner-border" role="status"><span class="visually-hidden">Loading...</span></div></div>`;
    const html = await fetch(url, { headers: { 'X-Requested-With': 'XMLHttpRequest' } }).then(r => r.text());
    container.innerHTML = html;
  }

  return { openModalFromUrl, confirm, postJson, loadTable };
})();
