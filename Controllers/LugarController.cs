using AutoMapper;
using GastosAPI.DTO;
using GastosAPI.Models;
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
    public class LugarController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly ILugarRepository _lugarRepository;

        public LugarController(IMapper mapper, ILugarRepository lugarRepository)
        {
            _mapper = mapper;
            _lugarRepository = lugarRepository;
        }

        [HttpGet]
        public async Task<IActionResult> getLugar()
        {
            ResponseApi<List<LugarDTO>> _responseApi = new ResponseApi<List<LugarDTO>>();

            try
            {
                List<LugarDTO> _lista = new List<LugarDTO>();
                _lista = _mapper.Map<List<LugarDTO>>(await _lugarRepository.Consultar());

                if (_lista.Count > 0)
                {
                    _responseApi = new ResponseApi<List<LugarDTO>>() { status = true, msg = "ok", value = _lista };
                }
                else
                    _responseApi = new ResponseApi<List<LugarDTO>>() { status = false, msg = "sin resultados", value = null };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<List<LugarDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpPost]
        public async Task<IActionResult> postLugar([FromBody] LugarDTO request)
        {
            ResponseApi<LugarDTO> _responseApi = new ResponseApi<LugarDTO>();

            try
            {
                Lugar _lugar = _mapper.Map<Lugar>(request);

                Lugar newLugar = await _lugarRepository.Crear(_lugar);
                if (newLugar != null)
                {
                    _responseApi = new ResponseApi<LugarDTO>() { status = true, msg = "ok", value = _mapper.Map<LugarDTO>(newLugar) };
                }
                else
                    _responseApi = new ResponseApi<LugarDTO>() { status = false, msg = "No se pudo crear la categoria" };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<LugarDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpPut]
        public async Task<IActionResult> putLugar([FromBody] LugarDTO request)
        {
            ResponseApi<LugarDTO> _responseApi = new ResponseApi<LugarDTO>();
            try
            {
                Lugar _lugar = _mapper.Map<Lugar>(request);
                Lugar updateLugar = await _lugarRepository.GetLugar(u => u.IdLugar == _lugar.IdLugar);

                if (updateLugar != null)
                {
                    updateLugar.NombreLugar = _lugar.NombreLugar;
                  

                    bool respuesta = await _lugarRepository.Editar(updateLugar);

                    if (respuesta)
                        _responseApi = new ResponseApi<LugarDTO>() { status = true, msg = "ok", value = _mapper.Map<LugarDTO>(updateLugar) };
                    else
                        _responseApi = new ResponseApi<LugarDTO>() { status = false, msg = "No se pudo editar la tarea" };
                }
                else
                {
                    _responseApi = new ResponseApi<LugarDTO>() { status = false, msg = "No se encontró la tarea" };
                }

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<LugarDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }
    }
}
