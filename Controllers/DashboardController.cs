﻿using AutoMapper;
using Azure;
using GastosAPI.DTO;
using GastosAPI.Repository.Contrato;
using GastosAPI.Repository.Implementacion;
using GastosAPI.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IMapper mapper, IDashboardRepository dashboardRepository)
        {
            _mapper = mapper;
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet]
        [Route("ultimosGastos")]
        public async Task<IActionResult> getUltimosGastos()
        {
            ResponseApi<List<TransaccionDTO>> _responseApi = new ResponseApi<List<TransaccionDTO>>();

            try
            {
                List<TransaccionDTO> _lista = new List<TransaccionDTO>();
                _lista = _mapper.Map<List<TransaccionDTO>>(await _dashboardRepository.UltimosGastos());

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
            ResponseApi<DashboardDTO> _responseApi = new ResponseApi<DashboardDTO>();

            try
            {
                DashboardDTO vmDashboard = new DashboardDTO();

                List<GastosSemanaDTO> listaVentasSemana = new List<GastosSemanaDTO>();

                foreach (KeyValuePair<string, int> item in await _dashboardRepository.GastosUltimasSemanas())
                {
                    listaVentasSemana.Add(new GastosSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }
                vmDashboard.GastosUltimaSemana = listaVentasSemana;

                _responseApi = new ResponseApi<DashboardDTO>() { status = true, msg = "ok", value = vmDashboard };
                return StatusCode(StatusCodes.Status200OK, _responseApi);

            }
            catch (Exception ex)
            {
                _responseApi = new ResponseApi<DashboardDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _responseApi);
            }
        }

        [HttpGet]
        [Route("gastosPorCategoriaPorDia")]

        public async Task<ActionResult<List<GastosPorCategoriaDTO>>> GetGastosPorCategoriaPorDia(DateTime fecha)
        {
            ResponseApi<List<GastosPorCategoriaDTO>> _responseApi = new ResponseApi<List<GastosPorCategoriaDTO>>();

            try
            {
                var gastosPorCategoria = await _dashboardRepository.GastosPorCategoriaPorDia(fecha);

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
    }
}
