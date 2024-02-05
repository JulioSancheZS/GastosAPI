using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Implementacion
{
    public class LugarRepository : ILugarRepository
    {
        private readonly GastosDbContext _dbContext;

        public LugarRepository(GastosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Lugar>> Consultar(Expression<Func<Lugar, bool>> filtro = null)
        {
            try
            {
                IQueryable<Lugar> query = filtro == null ? _dbContext.Lugars : _dbContext.Lugars.Where(filtro);
                return query;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Lugar> Crear(Lugar lugar)
        {
            try
            {
                _dbContext.Set<Lugar>().Add(lugar);
                await _dbContext.SaveChangesAsync();
                return lugar;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(Lugar lugar)
        {
            try
            {
                _dbContext.Update(lugar);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {

                throw;
            }
        }

        public async Task<Lugar> GetTransaccion(Expression<Func<Lugar, bool>> filtro)
        {
            try
            {
                return await _dbContext.Lugars.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {

                throw;
            }
        }
    }
}
