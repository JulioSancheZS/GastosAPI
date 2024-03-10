using AutoMapper;
using GastosAPI.DTO;
using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using GastosAPI.Repository.Implementacion;
using GastosAPI.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresosController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IIngresosRepository _ingresosRepository;

        public IngresosController(IMapper mapper, IIngresosRepository ingresosRepository)
        {
            _mapper = mapper;
            _ingresosRepository = ingresosRepository;
        }

        private Guid? ObtenerIdUsuarioDesdeToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario");

            if (claim != null && Guid.TryParse(claim.Value, out Guid idUsuario))
            {
                return idUsuario;
            }

            // Manejar el caso donde no se puede obtener el IdUsuario
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> getIngresos(DateTime mes)
        {
            ResponseApi<List<IngresosDTO>> _responseApi = new ResponseApi<List<IngresosDTO>>();

            try
            {
                //Guid? idUsuario = ObtenerIdUsuarioDesdeToken();

                List<IngresosDTO> _lista = new List<IngresosDTO>();
                IQueryable<Ingreso> query = await _ingresosRepository.Consultar(x => x.FechaIngreso!.Value.Year == mes.Year && x.FechaIngreso.Value.Month == mes.Month);

                _lista = _mapper.Map<List<IngresosDTO>>(query.ToList());

                if (_lista.Count > 0)
                {
                    _responseApi = new ResponseApi<List<IngresosDTO>>() { status = true, msg = "ok", value = _lista };
                }
                else
                    _responseApi = new ResponseApi<List<IngresosDTO>>() { status = false, msg = "sin resultados", value = null };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<List<IngresosDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpPost]
        public async Task<IActionResult> postIngresos([FromBody] IngresosDTO request)
        {
            ResponseApi<IngresosDTO> _responseApi = new ResponseApi<IngresosDTO>();

            try
            {
                Guid? idUsuario = ObtenerIdUsuarioDesdeToken();

                Ingreso _ingresos = _mapper.Map<Ingreso>(request);
                _ingresos.IdUsuario = idUsuario;
                Ingreso newIngreso = await _ingresosRepository.CrearIngresos(_ingresos);
                if (newIngreso != null)
                {
                    _responseApi = new ResponseApi<IngresosDTO>() { status = true, msg = "ok", value = _mapper.Map<IngresosDTO>(newIngreso) };
                }
                else
                    _responseApi = new ResponseApi<IngresosDTO>() { status = false, msg = "No se pudo crear el Ingreso" };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<IngresosDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
            //}

        }
    }
}
