using GastosAPI.Models;

namespace GastosAPI.Repository.Contrato
{
    public interface IDashboardRepository
    {
        Task<IQueryable<Transaccion>> UltimosGastos();
        Task<List<(decimal? TotalGastos, string? NombreCategoria)>> GastosPorCategoriaPorDia(DateTime fecha);
        Task<List<(decimal? TotalGastos, string? NombreCategoria)>> GastosPorCategoriaPorMes(DateTime fecha);
        Task<List<(decimal? TotalGastos, string? NombreCategoria)>> GastosPorCategoriaPorAnio (DateTime fecha);
        Task<Dictionary<string, (int totalTransacciones, decimal? totalGastos)>> GastosUltimasSemanas();
    }
}
