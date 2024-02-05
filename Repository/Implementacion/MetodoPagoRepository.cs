using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Implementacion
{
    public class MetodoPagoRepository : IMetodoPagoRepository
    {
        private readonly GastosDbContext _dbContext;

        public async Task<IQueryable<MetodoPago>> Consultar(Expression<Func<MetodoPago, bool>> filtro = null)
        {
            try
            {
                IQueryable<MetodoPago> query = filtro == null ? _dbContext.MetodoPagos : _dbContext.MetodoPagos.Where(filtro);
                return query;
            }
            catch
            {
                throw;
            }
        }

        public async Task<MetodoPago> Crear(MetodoPago metodoPago)
        {
            try
            {
                _dbContext.Set<MetodoPago>().Add(metodoPago);
                await _dbContext.SaveChangesAsync();
                return metodoPago;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(MetodoPago metodoPago)
        {
            try
            {
                _dbContext.Update(metodoPago);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {

                throw;
            }
        }

        public async Task<MetodoPago> GetTransaccion(Expression<Func<MetodoPago, bool>> filtro)
        {
            try
            {
                return await _dbContext.MetodoPagos.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {

                throw;
            }
        }
    }
}
