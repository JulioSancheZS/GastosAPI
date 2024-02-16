using AutoMapper;
using Azure;
using GastosAPI.DTO;
using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using GastosAPI.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace GastosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(IMapper mapper, ICategoriaRepository categoriaRepository)
        {
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> getCategoria()
        {
            ResponseApi<List<CategoriaDTO>> _responseApi = new ResponseApi<List<CategoriaDTO>>();

            try
            {
                List<CategoriaDTO> _lista = new List<CategoriaDTO>();
                _lista = _mapper.Map<List<CategoriaDTO>>(await _categoriaRepository.Consultar());

                if (_lista.Count > 0)
                {
                    _responseApi = new ResponseApi<List<CategoriaDTO>>() { status = true, msg = "ok", value = _lista };
                }
                else
                    _responseApi = new ResponseApi<List<CategoriaDTO>>() { status = false, msg = "sin resultados", value = null };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<List<CategoriaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpPost]
        public async Task<IActionResult> postCategoria([FromBody] CategoriaDTO request)
        {
            ResponseApi<CategoriaDTO> _responseApi = new ResponseApi<CategoriaDTO>();

            try
            {
                Categoria _categoria = _mapper.Map<Categoria>(request);

                Categoria newCategoria = await _categoriaRepository.Crear(_categoria);
                if (newCategoria != null)
                {
                    _responseApi = new ResponseApi<CategoriaDTO>() { status = true, msg = "ok", value = _mapper.Map<CategoriaDTO>(newCategoria) };
                }
                else
                    _responseApi = new ResponseApi<CategoriaDTO>() { status = false, msg = "No se pudo crear la categoria" };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<CategoriaDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpPut]
        public async Task<IActionResult> putCategoria([FromBody] CategoriaDTO request)
        {
            ResponseApi<CategoriaDTO> _responseApi = new ResponseApi<CategoriaDTO>();
            try
            {
                Categoria _categoria = _mapper.Map<Categoria>(request);
                Categoria updateCategoria = await _categoriaRepository.GetCategoria(u => u.IdCategoria == _categoria.IdCategoria);

                if (updateCategoria != null)
                {
                    updateCategoria.NombreCategoria = _categoria.NombreCategoria;
                    updateCategoria.Color = _categoria.Color;
                    updateCategoria.Icono = _categoria.Icono;

                    bool respuesta = await _categoriaRepository.Editar(updateCategoria);

                    if (respuesta)
                        _responseApi = new ResponseApi<CategoriaDTO>() { status = true, msg = "ok", value = _mapper.Map<CategoriaDTO>(updateCategoria) };
                    else
                        _responseApi = new ResponseApi<CategoriaDTO>() { status = false, msg = "No se pudo editar la tarea" };
                }
                else
                {
                    _responseApi = new ResponseApi<CategoriaDTO>() { status = false, msg = "No se encontró la tarea" };
                }

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<CategoriaDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }
    }
}
