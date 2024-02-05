using GastosAPI.Models;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Contrato
{
    public interface ITasaCambioRepository
    {
        Task<TasaCambio> Crear(TasaCambio tasaCambio);
        Task<IQueryable<TasaCambio>> Consultar(Expression<Func<TasaCambio, bool>> filtro = null);
    }
}
