using Shared.Api.BaseModel;
using Shared.Api.Exceptions;

namespace GerenciadorInventario.ProdutoAPI.Models;

public class Produto : BaseModel<Produto>
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string? Descricao { get; set; }

    public Produto() { }
    public Produto(string nome, decimal preco, string descricao)
    {
        this.Nome = nome;
        this.Preco = preco;
        this.Descricao = descricao;
    }

    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(this.Nome)) throw new DomainException("Por favor, inserir o nome do produto.");
        if (this.Preco <= 0) throw new DomainException("Preço deve ser maior que zero.");
    }

    public void Alterar(string nome, decimal preco, string descricao)
    {
        this.Nome = nome;
        this.Preco = preco;
        this.Descricao = descricao;
    }
}
