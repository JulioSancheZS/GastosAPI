using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using System.Linq;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Implementacion
{
    public class TasaCambioRepositoty : ITasaCambioRepository
    {
        private readonly GastosDbContext _dbContext;

        public TasaCambioRepositoty(GastosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<TasaCambio>> Consultar(Expression<Func<TasaCambio, bool>> filtro = null)
        {
            try
            {
                IQueryable<TasaCambio> query = filtro == null ? _dbContext.TasaCambios : _dbContext.TasaCambios.Where(filtro);
                return query;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TasaCambio> Crear(TasaCambio tasaCambio)
        {
            try
            {
                _dbContext.Set<TasaCambio>().Add(tasaCambio);
                await _dbContext.SaveChangesAsync();
                return tasaCambio;
            }
            catch
            {
                throw;
            }
        }
    }
}
