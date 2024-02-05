using GastosAPI.Models;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Contrato
{
    public interface IMetodoPagoRepository
    {
        Task<MetodoPago> Crear(MetodoPago metodoPago);
        Task<bool> Editar(MetodoPago metodoPago);
        Task<MetodoPago> GetTransaccion(Expression<Func<MetodoPago, bool>> filtro);
        Task<IQueryable<MetodoPago>> Consultar(Expression<Func<MetodoPago, bool>> filtro = null);
    }
}
