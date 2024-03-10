using AutoMapper;
using GastosAPI.DTO;
using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using GastosAPI.Repository.Implementacion;
using GastosAPI.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IBalanceRepository _balanceRepository;

        public BalanceController(IMapper mapper, IBalanceRepository balanceRepository)
        {
            _mapper = mapper;
            _balanceRepository = balanceRepository;
        }


        private Guid? ObtenerIdUsuarioDesdeToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario");

            if (claim != null && Guid.TryParse(claim.Value, out Guid idUsuario))
            {
                return idUsuario;
            }

            return null;

        }
        [HttpGet]
        public async Task<IActionResult> getBalanceByIdUsuario()
        {
            ResponseApi<BalanceDTO> _responseApi = new ResponseApi<BalanceDTO>();

            Guid? IdUsuario = ObtenerIdUsuarioDesdeToken();

            try
            {
                Balance findBalance = await _balanceRepository.GetBalance(x => x.IdUsuario == IdUsuario);

                if (findBalance != null)
                {
                    _responseApi = new ResponseApi<BalanceDTO>() { status = true, msg = "ok", value = _mapper.Map<BalanceDTO>(findBalance) };
                }
                else
                    _responseApi = new ResponseApi<BalanceDTO>() { status = false, msg = "sin resultados", value = null };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<BalanceDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }
    }
}
