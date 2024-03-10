using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Implementacion
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly GastosDbContext _dbContext;

        public BalanceRepository(GastosDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Balance> GetBalance(Expression<Func<Balance, bool>> filtro)
        {
            try
            {
                return await _dbContext.Balances.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
