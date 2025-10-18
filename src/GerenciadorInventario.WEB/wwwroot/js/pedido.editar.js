(function(){
  const root = document.getElementById('pedidoEditor');
  if(!root) return;
  const tbody = root.querySelector('#itensTable tbody');
  const totalEl = root.querySelector('#totalGeral');
  const produtosJson = document.getElementById('pedidoProdutosData')?.textContent || '[]';
  let produtos = [];
  try{ produtos = JSON.parse(produtosJson); }catch{ produtos = []; }

  function fmt(v){ return (v||0).toLocaleString('pt-BR', { style:'currency', currency:'BRL' }); }

  function calcTotal(){
    let tot = 0;
    tbody.querySelectorAll('tr').forEach(tr=>{ const n = parseFloat(tr.querySelector('[data-field="subtotal"]').dataset.value||'0'); tot += isNaN(n)?0:n; });
    totalEl.textContent = fmt(tot);
  }

  function addRow(defaults){
    const tr = document.createElement('tr');
    tr.innerHTML = `
<td>
  <select class="form-select form-select-sm" data-field="produto">
    <option value="">Selecione...</option>
    ${produtos.map(p=>`<option value="${p.id}" data-preco="${p.preco}">${p.nome}</option>`).join('')}
  </select>
</td>
<td class="text-end"><input type="number" min="1" value="${defaults?.quantidade||1}" class="form-control form-control-sm text-end" data-field="quantidade" /></td>
<td class="text-end"><span data-field="preco">${fmt(defaults?.precoUnitario||0)}</span></td>
<td class="text-end"><span data-field="subtotal" data-value="0">${fmt(0)}</span></td>
<td class="text-end"><button class="btn btn-outline-danger btn-sm" type="button" data-action="rm"><i class="fa-solid fa-trash"></i></button></td>`;
    tbody.appendChild(tr);

    const sel = tr.querySelector('[data-field="produto"]');
    const qty = tr.querySelector('[data-field="quantidade"]');
    const precoEl = tr.querySelector('[data-field="preco"]');
    const subEl = tr.querySelector('[data-field="subtotal"]');

    function recalc(){
      const opt = sel.selectedOptions[0];
      const preco = parseFloat(opt?.dataset?.preco||'0');
      const q = parseInt(qty.value||'0');
      const subtotal = (isNaN(preco)?0:preco) * (isNaN(q)?0:q);
      precoEl.textContent = fmt(preco);
      subEl.textContent = fmt(subtotal);
      subEl.dataset.value = subtotal;
      calcTotal();
    }

    sel.addEventListener('change', recalc);
    qty.addEventListener('input', recalc);
    tr.querySelector('[data-action="rm"]').addEventListener('click', ()=>{ tr.remove(); calcTotal(); });

    recalc();
  }

  root.querySelector('#btnAddItem').addEventListener('click', ()=> addRow({ quantidade: 1 }));

  document.getElementById('btnSalvarPedido').addEventListener('click', async ()=>{
    const itens = [];
    tbody.querySelectorAll('tr').forEach(tr=>{
      const sel = tr.querySelector('[data-field="produto"]');
      const produtoId = parseInt(sel.value||'0');
      if(!produtoId) return;
      const nome = sel.selectedOptions[0].textContent;
      const preco = parseFloat(sel.selectedOptions[0].dataset.preco||'0');
      const quantidade = parseInt(tr.querySelector('[data-field="quantidade"]').value||'0');
      itens.push({ produtoId, quantidade, precoUnitario: preco, produtoNome: nome });
    });
    if(itens.length === 0){ Ui.toast({ style:'warning', title:'Pedido', message:'Adicione pelo menos um item.' }); return; }
    const dto = { itens: itens.map(x=>({ produtoId:x.produtoId, quantidade:x.quantidade, precoUnitario:x.precoUnitario })) };
    const res = await Ui.postJson('/Pedido/Criar', dto);
    if(res){ document.querySelector('#modalHost .modal.show .btn-close')?.click(); window.PedidoPage.reloadTable(); }
  });

  // default: one row ready to edit
  addRow({ quantidade:1 });
})();
