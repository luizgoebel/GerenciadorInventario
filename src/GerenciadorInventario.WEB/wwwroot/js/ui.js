window.Ui = (function () {
  const modalHost = document.getElementById('modalHost');
  const confirmHost = document.getElementById('confirmHost');
  const toastHost = document.getElementById('toastHost');

  function t(key){ try { return window.I18n?.t(key) || key; } catch { return key; } }

  function initTooltips(root=document){
    const els = root.querySelectorAll?.('[data-bs-toggle="tooltip"]');
    els?.forEach(el => {
      try { new bootstrap.Tooltip(el, { container: 'body', boundary: 'window', placement: el.getAttribute('data-bs-placement') || 'top' }); } catch {}
    });
  }

  async function readError(resp){
    const ct = resp.headers.get('content-type') || '';
    let body;
    try {
      if (ct.includes('json')) body = await resp.json(); else body = await resp.text();
    } catch { /* ignore parse errors */ }

    if (!body) return `${resp.status} ${resp.statusText}`;

    if (typeof body === 'string') return body;

    const parts = [];
    if (body.title) parts.push(body.title);
    if (body.detail) parts.push(body.detail);
    if (body.message) parts.push(body.message);
    if (body.errors) {
      for (const k of Object.keys(body.errors)) {
        const arr = body.errors[k];
        if (Array.isArray(arr)) parts.push(`${k}: ${arr.join(', ')}`); else parts.push(`${k}: ${arr}`);
      }
    }
    const msg = parts.filter(Boolean).join(' \n ');
    return msg || `${resp.status} ${resp.statusText}`;
  }

  function toast({ title = '', message = '', style = 'primary', delay = 4000 }){
    if (!toastHost) return;
    const el = document.createElement('div');
    el.className = 'toast align-items-center text-bg-' + style + ' border-0';
    el.setAttribute('role','alert');
    el.setAttribute('aria-live','assertive');
    el.setAttribute('aria-atomic','true');
    el.innerHTML = `
  <div class="d-flex">
    <div class="toast-body">
      ${title ? `<div class=\"fw-semibold\">${title}</div>` : ''}
      <div style="white-space: pre-wrap;">${message}</div>
    </div>
    <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
  </div>`;
    toastHost.appendChild(el);
    const t = new bootstrap.Toast(el, { delay, autohide: true });
    t.show();
    el.addEventListener('hidden.bs.toast', ()=> el.remove());
    return el;
  }

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

  function executeInlineScripts(root){
    try{
      const scripts = root.querySelectorAll('script');
      scripts.forEach(old => {
        const type = (old.getAttribute('type')||'').toLowerCase();
        // Skip non-JS scripts (e.g., application/json with data for the modal)
        if (type && type !== 'text/javascript' && type !== 'module') {
          return; // keep it in place
        }
        const s = document.createElement('script');
        if (old.src) { s.src = old.src; }
        if (type) { s.type = type; }
        if (old.id) { s.id = old.id; }
        s.text = old.textContent || '';
        document.body.appendChild(s);
        old.parentNode?.removeChild(old);
      });
    }catch{}
  }

  async function openModalFromUrl(url, title) {
    const resp = await fetch(url, { headers: { 'X-Requested-With': 'XMLHttpRequest' } });
    if (!resp.ok) {
      const msg = await readError(resp);
      return null;
    }
    const html = await resp.text();
    const id = `mdl_${Date.now()}`;
    const modalEl = buildModal(id, title, html);
    modalHost.innerHTML = '';
    modalHost.appendChild(modalEl);
    // Execute any inline/external scripts included in the loaded partial
    executeInlineScripts(modalEl);
    const modal = new bootstrap.Modal(modalEl);
    modal.show();
    modalEl.addEventListener('shown.bs.modal', ()=> initTooltips(modalEl));
    return modalEl;
  }

  function confirm({ title = 'Confirmação', message = '', style = 'primary', confirmText = 'Confirmar', cancelText = 'Cancelar', onConfirm }) {
    const id = `confirm_${Date.now()}`;
    const footer = `
      <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">${cancelText}</button>
      <button type=\"button\" class=\"btn btn-${style}\" id=\"${id}_ok\">${confirmText}</button>`;
    const el = buildModal(id, title, `<div class=\"lead\">${message}</div>`, footer);
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
    try {
      const resp = await fetch(url, {
        method,
        headers: { 'Content-Type': 'application/json', 'X-Requested-With': 'XMLHttpRequest' },
        body: data ? JSON.stringify(data) : null
      });
      if (!resp.ok) {
        const msg = await readError(resp);
        toast({ title: t('toast.error'), message: msg, style: 'danger' });
        return null;
      }
      const ct = resp.headers.get('content-type') || '';
      if (ct.includes('application/json')) return await resp.json();
      const txt = await resp.text();
      return txt && txt.length ? txt : true;
    } catch (e) {
      toast({ title: t('toast.error'), message: (e?.message||e||'') + '', style: 'danger' });
      return null;
    }
  }

  async function loadTable({ url, containerId }) {
    const container = document.getElementById(containerId);
    if (!container) return;
    container.innerHTML = `<div class=\"text-center py-4\"><div class=\"spinner-border\" role=\"status\"><span class=\"visually-hidden\">Loading...</span></div></div>`;
    const resp = await fetch(url, { headers: { 'X-Requested-With': 'XMLHttpRequest' } });
    if (!resp.ok) {
      const msg = await readError(resp);
      container.innerHTML = `<div class=\"alert alert-danger\" role=\"alert\">${msg}</div>`;
      return;
    }
    const html = await resp.text();
    container.innerHTML = html;
    initTooltips(container);
  }

  document.addEventListener('DOMContentLoaded', ()=> initTooltips());

  return { openModalFromUrl, confirm, postJson, loadTable, initTooltips, toast };
})();
