using GastosAPI.Models;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Contrato
{
    public interface ILugarRepository
    {
        Task<Lugar> Crear(Lugar lugar);
        Task<bool> Editar(Lugar lugar);
        Task<Lugar> GetLugar(Expression<Func<Lugar, bool>> filtro);
        Task<IQueryable<Lugar>> Consultar(Expression<Func<Lugar, bool>> filtro = null);
    }
}
