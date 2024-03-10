using GastosAPI.Models;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Contrato
{
    public interface IBalanceRepository
    {
        Task<Balance> GetBalance(Expression<Func<Balance, bool>> filtro);

    }
}
