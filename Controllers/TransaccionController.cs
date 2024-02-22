﻿using AutoMapper;
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
    public class TransaccionController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly ITransaccionRepository _transaccionRepository;

        public TransaccionController(IMapper mapper, ITransaccionRepository transaccionRepository)
        {
            _mapper = mapper;
            _transaccionRepository = transaccionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> getTransaccion(DateTime mes)
        {
            ResponseApi<List<TransaccionDTO>> _responseApi = new ResponseApi<List<TransaccionDTO>>();

            try
            {
                List<TransaccionDTO> _lista = new List<TransaccionDTO>();
                IQueryable<Transaccion> query = await _transaccionRepository.Consultar(x => x.FechaTransaccion!.Value.Year == mes.Year && x.FechaTransaccion.Value.Month == mes.Month);
                query = query.Include(r => r.IdCategoriaNavigation).Include(x => x.IdMetodoPagoNavigation).Include(x => x.IdLugarNavigation).OrderBy(x => x.FechaTransaccion);

                _lista = _mapper.Map<List<TransaccionDTO>>(query.ToList());

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

        [HttpPost]
        public async Task<IActionResult> postTransaccion([FromBody] TransaccionDTO request)
        {
            ResponseApi<TransaccionDTO> _responseApi = new ResponseApi<TransaccionDTO>();

            try
            {
                Transaccion _transaccion = _mapper.Map<Transaccion>(request);

                Transaccion newTransaccion = await _transaccionRepository.CrearTransaccion(_transaccion);
                if (newTransaccion != null)
                {
                    _responseApi = new ResponseApi<TransaccionDTO>() { status = true, msg = "ok", value = _mapper.Map<TransaccionDTO>(newTransaccion) };
                }
                else
                    _responseApi = new ResponseApi<TransaccionDTO>() { status = false, msg = "No se pudo crear la categoria" };

                return StatusCode(StatusCodes.Status200OK, _responseApi);
            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<TransaccionDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }
    }
}
