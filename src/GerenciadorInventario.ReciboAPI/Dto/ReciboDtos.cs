namespace GerenciadorInventario.ReciboAPI.Dto;

public record ReciboDto(int Id, string Numero, int FaturaId, DateTime DataEmissao, decimal ValorTotal);
