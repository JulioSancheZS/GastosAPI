using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Implementacion
{
    public class TransaccionRepository : ITransaccionRepository
    {
        public readonly GastosDbContext _dbContext;

        public TransaccionRepository(GastosDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public async Task<bool> Anular(Transaccion transaccion)
        {
            try
            {
                _dbContext.Update(transaccion);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<IQueryable<Transaccion>> Consultar(Expression<Func<Transaccion, bool>> filtro = null)
        {
            try
            {
                IQueryable<Transaccion> query = filtro == null ? _dbContext.Transaccions : _dbContext.Transaccions.Where(filtro);
                return query;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Transaccion> CrearTransaccion(Transaccion transaccion)
        {
            try
            {
                transaccion.IdTransaccion = Guid.NewGuid();
                Balance balance = await _dbContext.Balances.FirstOrDefaultAsync(s => s.IdUsuario == transaccion.IdUsuario);
                if (balance != null)
                {
                    balance.Monto -= transaccion.Monto;
                    //balance.FechaActualizacion = DateTime.Now;
                    _dbContext.Update(balance);

                }
                _dbContext.Set<Transaccion>().Add(transaccion);
                await _dbContext.SaveChangesAsync();
                return transaccion;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<bool> Editar(Transaccion transaccion)
        {
            try
            {
                _dbContext.Update(transaccion);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {

                throw;
            }
        }

        public async Task<Transaccion> GetTransaccion(Expression<Func<Transaccion, bool>> expression)
        {
            try
            {
                return await _dbContext.Transaccions.Where(expression).FirstOrDefaultAsync();
            }
            catch 
            {
                throw;
            }
        }
    }
}
