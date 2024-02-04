using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Implementacion
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        public readonly GastosDbContext _dbcontext;
        public AuthenticationRepository(GastosDbContext dbContext) 
        { 
            _dbcontext = dbContext;
        }
        public async Task<Usuario> Registro(Usuario usuario)
        {
            try
            {
                string hashPassword = HashPassword(usuario.Password, out string salt);
                usuario.Password = hashPassword;
                usuario.PaswordHash = salt;

                usuario.FechaRegistro = DateTime.Now;
                usuario.IdUsuario = Guid.NewGuid();

                _dbcontext.Set<Usuario>().Add(usuario);
                await _dbcontext.SaveChangesAsync();
                return usuario;
            }
            catch 
            {
                throw;
            }

        }

        static string HashPassword(string password, out string salt)
        {
            // Genera un valor aleatorio (sal) para cada contraseña
            salt = BCrypt.Net.BCrypt.GenerateSalt(12);

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        public async Task<Usuario> ObtenerUsuario(Expression<Func<Usuario, bool>> filtro)
        {
            try
            {
                return await _dbcontext.Usuarios.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {

                throw;
            }
        }
    }
}
