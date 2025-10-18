namespace GerenciadorInventario.WEB.Dtos;

public record ReciboDto(int Id, string Numero, int FaturaId, DateTime DataEmissao, decimal ValorTotal);
