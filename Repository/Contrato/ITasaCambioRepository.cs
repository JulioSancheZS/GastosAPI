using GastosAPI.Models;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Contrato
{
    public interface ITasaCambioRepository
    {
        Task<IQueryable<TasaCambio>> Consultar(Expression<Func<TasaCambio, bool>> filtro = null);
        Task<bool> InsetarMes(DateTime fecha);
        Task<TasaCambio> GetTasaCambio(DateTime fecha);
    }
}
