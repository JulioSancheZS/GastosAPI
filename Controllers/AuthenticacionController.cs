using AutoMapper;
using GastosAPI.DTO;
using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using GastosAPI.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GastosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticacionController : ControllerBase
    {
        public readonly IAuthenticationRepository _authenticationRepository;
        public readonly IMapper _mapper;
        public readonly IConfiguration _configuration;

        public AuthenticacionController(IAuthenticationRepository authenticationRepository, IMapper mapper, IConfiguration configuration)
        {
            _authenticationRepository = authenticationRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO usuarioDTO)
        {
            ResponseApi<RegisterDTO> _responseApi = new ResponseApi<RegisterDTO>();

            try
            {

                Usuario findUser = await _authenticationRepository.ObtenerUsuario(x => x.Correo == usuarioDTO.Correo);

                if (findUser != null)
                {
                    _responseApi = new ResponseApi<RegisterDTO>() { status = true, msg = "El corre ya existe, intente con otro correo", value = null };
                    return StatusCode(StatusCodes.Status200OK, _responseApi);
                }

                Usuario _usuario = _mapper.Map<Usuario>(usuarioDTO);
                Usuario _newUsuario = await _authenticationRepository.Registro(_usuario);

                if (_newUsuario != null)
                {
                    _responseApi = new ResponseApi<RegisterDTO>() { status = true, msg = "Se ha registrado correctamente", value = _mapper.Map<RegisterDTO>(_newUsuario) };
                }
                else
                    _responseApi = new ResponseApi<RegisterDTO>() { status = false, msg = "No se pudo registrar su usuario" };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<RegisterDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            ResponseApi<UsuarioDTO> _responseApi = new ResponseApi<UsuarioDTO>();

            try
            {
                Usuario _usuario = await _authenticationRepository.ObtenerUsuario(x => x.Correo == loginDTO.Correo);
               
                if (_usuario == null || !VerifyPassword(loginDTO.Password, _usuario.Password, _usuario.PaswordHash))
                {
                    _responseApi = new ResponseApi<UsuarioDTO> { status = false, msg = "Credenciales inválidas. Intenta nuevamente.", value = null };
                    return StatusCode(StatusCodes.Status200OK, _responseApi);
                }

                //JWT
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("IdUsuario", _usuario.IdUsuario.ToString()),
                        new Claim("Nombre", _usuario.PrimerNombre + " " + _usuario.SegundoNombre),
                        new Claim("Correo", _usuario.Correo)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"].PadRight(16)));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signIn);

                _responseApi = new ResponseApi<UsuarioDTO>() { status = true, msg = "ok", value = _mapper.Map<UsuarioDTO>(_usuario), token = new JwtSecurityTokenHandler().WriteToken(token) };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<UsuarioDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        // Método para verificar una contraseña con el hash almacenado y la sal
        static bool VerifyPassword(string userInputPassword, string storedHashedPassword, string storedSalt)
        {
            // Combina la contraseña ingresada con la sal almacenada, luego verifica el hash
            string combinedPassword = BCrypt.Net.BCrypt.HashPassword(userInputPassword, storedSalt);
            return combinedPassword == storedHashedPassword;
        }
    }
}
