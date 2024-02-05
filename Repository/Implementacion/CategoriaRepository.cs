using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Implementacion
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly GastosDbContext _dbContext;

        public CategoriaRepository(GastosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Categoria>> Consultar(Expression<Func<Categoria, bool>> filtro = null)
        {
            try
            {
                IQueryable<Categoria> query = filtro == null ? _dbContext.Categoria : _dbContext.Categoria.Where(filtro);
                return query;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Categoria> Crear(Categoria categoria)
        {
            try
            {
                _dbContext.Set<Categoria>().Add(categoria);
                await _dbContext.SaveChangesAsync();
                return categoria;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(Categoria categoria)
        {
            try
            {
                _dbContext.Update(categoria);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {

                throw;
            }

        }

        public async Task<Categoria> GetTransaccion(Expression<Func<Categoria, bool>> filtro)
        {
            try
            {
                return await _dbContext.Categoria.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {

                throw;
            }
        }
    }
}
