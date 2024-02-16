using GastosAPI.Models;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Contrato
{
    public interface ICategoriaRepository
    {
        Task<Categoria> Crear(Categoria categoria);
        Task<bool> Editar(Categoria categoria);
        Task<Categoria> GetCategoria(Expression<Func<Categoria, bool>> filtro);
        Task<IQueryable<Categoria>> Consultar(Expression<Func<Categoria, bool>> filtro = null);
    }
}
