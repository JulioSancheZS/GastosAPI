using GastosAPI.Models;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Contrato
{
    public interface ITransaccionRepository
    {
        Task<Transaccion> CrearTransaccion(Transaccion transaccion);
        Task<Transaccion> GetTransaccion(Expression <Func<Transaccion, bool>> expression);
        Task<bool> Editar(Transaccion transaccion);
        Task<bool> Anular(Transaccion transaccion);
        Task<IQueryable<Transaccion>> Consultar(Expression<Func<Transaccion, bool>> filtro = null);
    }
}
