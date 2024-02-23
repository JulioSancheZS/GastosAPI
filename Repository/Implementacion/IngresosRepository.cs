using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Implementacion
{
    public class IngresosRepository : IIngresosRepository
    {
        public readonly GastosDbContext _dbContext;

        public IngresosRepository(GastosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Anular(Ingreso ingresos)
        {
            try
            {
                _dbContext.Update(ingresos);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<Ingreso>> Consultar(Expression<Func<Ingreso, bool>> filtro = null)
        {
            try
            {
                IQueryable<Ingreso> query = filtro == null ? _dbContext.Ingresos : _dbContext.Ingresos.Where(filtro);
                return query;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Ingreso> CrearIngresos(Ingreso ingresos)
        {
            try
            {
                
                ingresos.IdIngreso = Guid.NewGuid();

                // Verificar si el usuario tiene balance 
                bool usuarioTieneBalance = await _dbContext.Balances.AnyAsync(b => b.IdUsuario == ingresos.IdUsuario);

                if (!usuarioTieneBalance)
                {
                    // Si el usuario no tiene balance, creamos un nuevo Balance
                    Balance nuevoBalance = new Balance
                    {
                        IdBalance = Guid.NewGuid(),
                        IdUsuario = ingresos.IdUsuario,
                        Monto = ingresos.Monto, 
                        FechaActualizacion = DateTime.Now
                    };

                    _dbContext.Set<Balance>().Add(nuevoBalance);
                    ingresos.IdBalance = nuevoBalance.IdBalance;

                }
                else
                {
                    var balance = await _dbContext.Balances.FirstOrDefaultAsync(b => b.IdUsuario == ingresos.IdUsuario);
                    balance.Monto += ingresos.Monto; 
                    balance.FechaActualizacion = DateTime.Now;
                    ingresos.IdBalance = balance.IdBalance;

                    _dbContext.Update(balance);

                }

               
                _dbContext.Set<Ingreso>().Add(ingresos);
                await _dbContext.SaveChangesAsync();
                return ingresos;
            }
            catch
            {
                throw;
            }
        }


        public async Task<Ingreso> GetIngresos(Expression<Func<Ingreso, bool>> ingresos)
        {
            try
            {
                return await _dbContext.Ingresos.Where(ingresos).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
