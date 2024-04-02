using GastosAPI.Models;

namespace GastosAPI.Repository.Contrato
{
    public interface IDashboardRepository
    {
        //Recorda pasar el Id Usuario
        Task<IQueryable<Transaccion>> UltimosGastos(Guid? IdUsuario);
        Task<List<(decimal? TotalGastos, string? NombreCategoria)>> GastosPorCategoriaPorFecha(DateTime fecha, Guid? IdUsuario);
        Task<List<(decimal? TotalGastos, string? NombreCategoria)>> GastosPorCategoriaPorMes(DateTime fecha);
        Task<List<(decimal? TotalGastos, string? NombreCategoria)>> GastosPorCategoriaPorAnio (DateTime fecha);
        Task<Dictionary<string, (int totalTransacciones, decimal? totalGastos)>> GastosUltimasSemanas(Guid? IdUsuario);
        Task<int> TotalNumGastos(Guid? IdUsuario);
        Task<decimal> TotalGastosDinero(Guid? IdUsuario);
    }
}
