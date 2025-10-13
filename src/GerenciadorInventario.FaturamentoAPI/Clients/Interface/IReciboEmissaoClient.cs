namespace GerenciadorInventario.FaturamentoAPI.Clients.Interface;

public interface IReciboEmissaoClient
{
    Task<bool> EmitirReciboAsync(int faturaId, string numeroFatura, decimal valorTotal);
}
