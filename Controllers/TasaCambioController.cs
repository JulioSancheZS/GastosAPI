using AutoMapper;
using GastosAPI.DTO;
using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using GastosAPI.Utilidades;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GastosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasaCambioController : ControllerBase
    {
        public readonly ITasaCambioRepository _tasaCambioRepository;
        public readonly IMapper _mapper;

        public TasaCambioController(ITasaCambioRepository tasaCambioRepository, IMapper mapper)
        {
            _tasaCambioRepository = tasaCambioRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/postTasaCambioMes")]
        public async Task<IActionResult> postTasaCambioMes()
        {
            ResponseApi<TasaCambioDTO> _responseApi = new ResponseApi<TasaCambioDTO>();

            try
            {
                DateTime dateTime = DateTime.Now;

                bool tasa = await _tasaCambioRepository.InsetarMes(dateTime);

                if (tasa != null)
                {
                    _responseApi = new ResponseApi<TasaCambioDTO>() { status = true, msg = "ok", value = _mapper.Map<TasaCambioDTO>(tasa) };
                }

                else
                    _responseApi = new ResponseApi<TasaCambioDTO>() { status = false, msg = "No se pudo descargar la tasa cambio del mess" };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {

                _responseApi = new ResponseApi<TasaCambioDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpGet]
        public async Task<IActionResult> getTasaDia()
        {
            ResponseApi<TasaCambioDTO> _responseApi = new ResponseApi<TasaCambioDTO>();

            try
            {
                DateTime dateTime = DateTime.Now;

                TasaCambio tasaCambio = await _tasaCambioRepository.GetTasaCambio(dateTime);

                if (tasaCambio != null)
                {
                    _responseApi = new ResponseApi<TasaCambioDTO>() { status = true, msg = "ok", value = _mapper.Map<TasaCambioDTO>(tasaCambio) };
                }else
                    _responseApi = new ResponseApi<TasaCambioDTO>() { status = false, msg = "No se pudo obtener la tasa cambio del día de hoy" };
                return StatusCode(StatusCodes.Status200OK, _responseApi);

            }
            catch(Exception ex)
            {
                _responseApi = new ResponseApi<TasaCambioDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }
    }
}
