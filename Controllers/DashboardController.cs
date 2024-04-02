using AutoMapper;
using Azure;
using GastosAPI.DTO;
using GastosAPI.Repository.Contrato;
using GastosAPI.Repository.Implementacion;
using GastosAPI.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IMapper mapper, IDashboardRepository dashboardRepository)
        {
            _mapper = mapper;
            _dashboardRepository = dashboardRepository;
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
        [Route("ultimosGastos")]
        public async Task<IActionResult> getUltimosGastos()
        {
            ResponseApi<List<TransaccionDTO>> _responseApi = new ResponseApi<List<TransaccionDTO>>();

            try
            {
                Guid? idUsuario = ObtenerIdUsuarioDesdeToken();

                List<TransaccionDTO> _lista = new List<TransaccionDTO>();
                _lista = _mapper.Map<List<TransaccionDTO>>(await _dashboardRepository.UltimosGastos(idUsuario));
                

                if (_lista.Count > 0)
                {
                    _responseApi = new ResponseApi<List<TransaccionDTO>>() { status = true, msg = "ok", value = _lista };
                }
                else
                    _responseApi = new ResponseApi<List<TransaccionDTO>>() { status = false, msg = "sin resultados", value = null };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<List<TransaccionDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }



        [HttpGet]
        [Route("resumen")]
        public async Task<IActionResult> Resumen()
        {
            ResponseApi<List<GastosSemanaDTO>> _responseApi = new ResponseApi<List<GastosSemanaDTO>>();

            try
            {
                DashboardDTO vmDashboard = new DashboardDTO();
                Guid? idUsuario = ObtenerIdUsuarioDesdeToken();

                List<GastosSemanaDTO> listaVentasSemana = new List<GastosSemanaDTO>();

                foreach (KeyValuePair<string, (int totalTransacciones, decimal? totalGastos)> item in await _dashboardRepository.GastosUltimasSemanas(idUsuario))
                {
                    listaVentasSemana.Add(new GastosSemanaDTO()
                    {
                        Fecha = item.Key,
                        TotalTransacciones = item.Value.totalTransacciones,
                        TotalGastos = item.Value.totalGastos
                    });
                }
                vmDashboard.GastosUltimaSemana = listaVentasSemana;

                _responseApi = new ResponseApi<List<GastosSemanaDTO>>() { status = true, msg = "ok", value = listaVentasSemana };
                return StatusCode(StatusCodes.Status200OK, _responseApi);

            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<List<GastosSemanaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpGet]
        [Route("gastosPorCategoriaPorFecha")]
        public async Task<ActionResult<List<GastosPorCategoriaDTO>>> GetGastosPorCategoriaPorFecha(DateTime fecha)
        {
            ResponseApi<List<GastosPorCategoriaDTO>> _responseApi = new ResponseApi<List<GastosPorCategoriaDTO>>();

            try
            {
                Guid? idUsuario = ObtenerIdUsuarioDesdeToken();

                var gastosPorCategoria = await _dashboardRepository.GastosPorCategoriaPorFecha(fecha, idUsuario);

                if (gastosPorCategoria.Count > 0)
                {
                    var gastosPorCategoriaDTO = gastosPorCategoria.Select(g => new GastosPorCategoriaDTO
                    {
                        TotalGastos = g.TotalGastos,
                        NombreCategoria = g.NombreCategoria
                    }).ToList();
                    _responseApi = new ResponseApi<List<GastosPorCategoriaDTO>>() { status = true, msg = "ok", value = gastosPorCategoriaDTO };
                }
                else
                {
                    _responseApi = new ResponseApi<List<GastosPorCategoriaDTO>>() { status = true, msg = "Sin Resultados", value = null };

                }


                return StatusCode(StatusCodes.Status200OK, _responseApi);

            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<List<GastosPorCategoriaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpGet]
        [Route("gastosPorCategoriaPorMes")]

        public async Task<ActionResult<List<GastosPorCategoriaDTO>>> GetGastosPorCategoriaPorMes(DateTime fecha)
        {
            ResponseApi<List<GastosPorCategoriaDTO>> _responseApi = new ResponseApi<List<GastosPorCategoriaDTO>>();

            try
            {
                var gastosPorCategoria = await _dashboardRepository.GastosPorCategoriaPorMes(fecha);

                var gastosPorCategoriaDTO = gastosPorCategoria.Select(g => new GastosPorCategoriaDTO
                {
                    TotalGastos = g.TotalGastos,
                    NombreCategoria = g.NombreCategoria
                }).ToList();

                _responseApi = new ResponseApi<List<GastosPorCategoriaDTO>>() { status = true, msg = "ok", value = gastosPorCategoriaDTO };
                return StatusCode(StatusCodes.Status200OK, _responseApi);

            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<List<GastosPorCategoriaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpGet]
        [Route("gastosPorCategoriaPorAnio")]

        public async Task<ActionResult<List<GastosPorCategoriaDTO>>> GetGastosPorCategoriaPorAnio(DateTime fecha)
        {
            ResponseApi<List<GastosPorCategoriaDTO>> _responseApi = new ResponseApi<List<GastosPorCategoriaDTO>>();

            try
            {
                var gastosPorCategoria = await _dashboardRepository.GastosPorCategoriaPorAnio(fecha);

                var gastosPorCategoriaDTO = gastosPorCategoria.Select(g => new GastosPorCategoriaDTO
                {
                    TotalGastos = g.TotalGastos,
                    NombreCategoria = g.NombreCategoria
                }).ToList();

                _responseApi = new ResponseApi<List<GastosPorCategoriaDTO>>() { status = true, msg = "ok", value = gastosPorCategoriaDTO };
                return StatusCode(StatusCodes.Status200OK, _responseApi);

            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<List<GastosPorCategoriaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpGet]
        [Route("totalNumGastos")]
        public async Task<IActionResult> GetNumGastosMes()
        {
            ResponseApi<int> _responseApi = new ResponseApi<int>();
            try
            {
                Guid? idUsuario = ObtenerIdUsuarioDesdeToken();

                var numGastos = await _dashboardRepository.TotalNumGastos(idUsuario);

                _responseApi = new ResponseApi<int>() { status = true, msg = "ok", value = numGastos };
                return StatusCode(StatusCodes.Status200OK, _responseApi);

            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<int>() { status = false, msg = ex.Message, value = 0 };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpGet]
        [Route("totalGastos")]
        public async Task<IActionResult> GetTotalGastosMes()
        {
            ResponseApi<decimal> _responseApi = new ResponseApi<decimal>();
            try
            {
                Guid? idUsuario = ObtenerIdUsuarioDesdeToken();

                var totalGastos = await _dashboardRepository.TotalGastosDinero(idUsuario);


                _responseApi = new ResponseApi<decimal>() { status = true, msg = "ok", value = totalGastos };
                return StatusCode(StatusCodes.Status200OK, _responseApi);

            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<decimal>() { status = false, msg = ex.Message, value = 0 };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }
    }
}
