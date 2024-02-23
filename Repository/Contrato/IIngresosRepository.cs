using GastosAPI.Models;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Contrato
{
    public interface IIngresosRepository
    {
        Task<Ingreso> CrearIngresos(Ingreso ingresos);
        Task<Ingreso> GetIngresos(Expression<Func<Ingreso, bool>> ingresos);
        Task<bool> Anular(Ingreso ingresos);
        Task<IQueryable<Ingreso>> Consultar(Expression<Func<Ingreso, bool>> filtro = null);
    }
}
