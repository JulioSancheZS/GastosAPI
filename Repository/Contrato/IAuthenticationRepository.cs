using GastosAPI.Models;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Contrato
{
    public interface IAuthenticationRepository
    {
        Task<Usuario> Registro(Usuario usuario);

        Task<Usuario> ObtenerUsuario(Expression<Func<Usuario, bool>> filtro);
    }
}
