using GastosAPI.Models;

namespace GastosAPI.Repository.Contrato
{
    public interface IDashboardRepository
    {
        //Recorda pasar el Id Usuario
        Task<IQueryable<Transaccion>> UltimosGastos();
        Task<List<(decimal? TotalGastos, string? NombreCategoria)>> GastosPorCategoriaPorFecha(DateTime fecha);
        Task<List<(decimal? TotalGastos, string? NombreCategoria)>> GastosPorCategoriaPorMes(DateTime fecha);
        Task<List<(decimal? TotalGastos, string? NombreCategoria)>> GastosPorCategoriaPorAnio (DateTime fecha);
        Task<Dictionary<string, (int totalTransacciones, decimal? totalGastos)>> GastosUltimasSemanas();
        Task<int> TotalNumGastos();
        Task<decimal> TotalGastosDinero();
    }
}
