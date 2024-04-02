using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GastosAPI.Repository.Implementacion
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly GastosDbContext _context;

        public DashboardRepository(GastosDbContext context)
        {
            _context = context;
        }
        //Gastos Por Fecha
        public async Task<List<(decimal? TotalGastos, string? NombreCategoria)>> GastosPorCategoriaPorFecha(DateTime fecha, Guid? IdUsuario)
        {
            try
            {
                var gastosPorCategoria = await _context.Transaccions
                .Where(t =>  t.FechaTransaccion.Value == fecha && t.IdUsuario == IdUsuario)
                    .GroupBy(t => t.IdCategoriaNavigation.NombreCategoria)
                    .Select(g => new
                    {
                        TotalGastos = g.Sum(t => t.Monto),
                        NombreCategoria = g.Key
                    })
                    .ToListAsync();

                return gastosPorCategoria
                    .Select(item => (item.TotalGastos, item.NombreCategoria))
                    .ToList();
            }
            catch 
            {
                throw;
            }
        }
       
        public async Task<Dictionary<string, (int totalTransacciones, decimal? totalGastos)>> GastosUltimasSemanas(Guid? IdUsuario)
        {
            Dictionary<string, (int totalTransacciones, decimal? totalGastos)> resultado = new Dictionary<string, (int totalTransacciones, decimal? totalGastos)>();
            try
            {
                IQueryable<Transaccion> _trasaccionesQuery = _context.Transaccions.AsQueryable();

                if (_trasaccionesQuery.Count() > 0)
                {
                    DateTime? ultimaFecha = _context.Transaccions.OrderByDescending(x => x.FechaTransaccion).Select(x => x.FechaTransaccion).FirstOrDefault();
                    ultimaFecha = ultimaFecha.Value.AddDays(-7);

                    IQueryable<Transaccion> query = _context.Transaccions.Where(v => v.FechaTransaccion.Value.Date >= ultimaFecha.Value.Date && v.IdUsuario == IdUsuario);

                    // Ejecutar la primera operación de lectura y almacenar los resultados en una lista
                    var resultados = await query.ToListAsync();

                    // Procesar los resultados almacenados en la lista
                    resultado = resultados
                        .GroupBy(v => v.FechaTransaccion.Value.Date)
                        .OrderBy(g => g.Key)
                        .Select(dv => new { 
                            fecha = dv.Key.ToString("dd/MM/yyyy"),
                            totalTransacciones = dv.Count(),
                            totalGastos = dv.Sum(t => t.Monto)
                        })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => (r.totalTransacciones, r.totalGastos));
                }

                return resultado;
            }
            catch
            {
                throw;
            }
        }


        public async Task<IQueryable<Transaccion>> UltimosGastos(Guid? IdUsuario)
        {
            try
            {
                IQueryable<Transaccion> query = _context.Transaccions.OrderByDescending(x => x.FechaTransaccion).Take(5).Where(u => u.IdUsuario == IdUsuario);
                return query;
            }
            catch
            {

                throw;
            }
        }


        public async Task<List<(decimal? TotalGastos, string? NombreCategoria)>> GastosPorCategoriaPorMes(DateTime fecha)
        {
            try
            {
                var gastosPorCategoria = await _context.Transaccions
                .Where(t => t.FechaTransaccion.Value.Month == fecha.Month)
                    .GroupBy(t => t.IdCategoriaNavigation.NombreCategoria)
                    .Select(g => new
                    {
                        TotalGastos = g.Sum(t => t.Monto),
                        NombreCategoria = g.Key
                    })
                    .ToListAsync();

                return gastosPorCategoria
                    .Select(item => (item.TotalGastos, item.NombreCategoria))
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<(decimal? TotalGastos, string? NombreCategoria)>> GastosPorCategoriaPorAnio(DateTime fecha)
        {
            try
            {
                var gastosPorCategoria = await _context.Transaccions
                .Where(t => t.FechaTransaccion.Value.Year == fecha.Year)
                    .GroupBy(t => t.IdCategoriaNavigation.NombreCategoria)
                    .Select(g => new
                    {
                        TotalGastos = g.Sum(t => t.Monto),
                        NombreCategoria = g.Key
                    })
                    .ToListAsync();

                return gastosPorCategoria
                    .Select(item => (item.TotalGastos, item.NombreCategoria))
                    .ToList();
            }
            catch
            {
                throw;
            }
        }
        //Total Gastos del mes Actual
        public async Task<int> TotalNumGastos(Guid? IdUsuario)
        {
            try
            {
                //Mes Actual
                DateTime now = DateTime.Now;

                var totalNumGastos = await _context.Transaccions.Where(x => x.FechaTransaccion.Value.Month == now.Month && x.IdUsuario == IdUsuario).CountAsync();

                return totalNumGastos;
            }
            catch 
            {

                throw;
            }
        }

        public async Task<decimal> TotalGastosDinero(Guid? IdUsuario)
        {
            try
            {
                DateTime now =  DateTime.Now;
                decimal? totalMesGastos = await _context.Transaccions.Where(x => x.FechaTransaccion.Value.Month == now.Month && x.IdUsuario == IdUsuario).SumAsync(t => t.Monto);
            
                return (decimal)totalMesGastos;
            }
            catch 
            {

                throw;
            }
        }
    }
}
