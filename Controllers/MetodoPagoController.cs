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
    public class MetodoPagoController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMetodoPagoRepository _metodoPagoRepository;

        public MetodoPagoController(IMapper mapper, IMetodoPagoRepository metodoPagoRepository)
        {
            _mapper = mapper;
            _metodoPagoRepository = metodoPagoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> getMetodoPago()
        {
            ResponseApi<List<MetodoPagoDTO>> _responseApi = new ResponseApi<List<MetodoPagoDTO>>();

            try
            {
                List<MetodoPagoDTO> _lista = new List<MetodoPagoDTO>();
                _lista = _mapper.Map<List<MetodoPagoDTO>>(await _metodoPagoRepository.Consultar());

                if (_lista.Count > 0)
                {
                    _responseApi = new ResponseApi<List<MetodoPagoDTO>>() { status = true, msg = "ok", value = _lista };
                }
                else
                    _responseApi = new ResponseApi<List<MetodoPagoDTO>>() { status = false, msg = "sin resultados", value = null };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<List<MetodoPagoDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpPost]
        public async Task<IActionResult> postMetodoPago([FromBody] MetodoPagoDTO request)
        {
            ResponseApi<MetodoPagoDTO> _responseApi = new ResponseApi<MetodoPagoDTO>();

            try
            {
                MetodoPago _metdoPago = _mapper.Map<MetodoPago>(request);

                MetodoPago newMetodoPago = await _metodoPagoRepository.Crear(_metdoPago);
                if (newMetodoPago != null)
                {
                    _responseApi = new ResponseApi<MetodoPagoDTO>() { status = true, msg = "ok", value = _mapper.Map<MetodoPagoDTO>(newMetodoPago) };
                }
                else
                    _responseApi = new ResponseApi<MetodoPagoDTO>() { status = false, msg = "No se pudo crear la categoria" };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<MetodoPagoDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpPut]
        public async Task<IActionResult> putMetodoPago([FromBody] MetodoPagoDTO request)
        {
            ResponseApi<MetodoPagoDTO> _responseApi = new ResponseApi<MetodoPagoDTO>();
            try
            {
                MetodoPago _metodoPago = _mapper.Map<MetodoPago>(request);
                MetodoPago updateMetodoPago = await _metodoPagoRepository.GetMetodoPago(u => u.IdMetodoPago == _metodoPago.IdMetodoPago);

                if (updateMetodoPago != null)
                {
                    updateMetodoPago.Descripcion = _metodoPago.Descripcion;


                    bool respuesta = await _metodoPagoRepository.Editar(updateMetodoPago);

                    if (respuesta)
                        _responseApi = new ResponseApi<MetodoPagoDTO>() { status = true, msg = "ok", value = _mapper.Map<MetodoPagoDTO>(updateMetodoPago) };
                    else
                        _responseApi = new ResponseApi<MetodoPagoDTO>() { status = false, msg = "No se pudo editar la tarea" };
                }
                else
                {
                    _responseApi = new ResponseApi<MetodoPagoDTO>() { status = false, msg = "No se encontró la tarea" };
                }

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<MetodoPagoDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }
    }
}
