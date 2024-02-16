using GastosAPI.Models;
using GastosAPI.Repository.Contrato;
using ServiceReference;
using System.Linq;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using System.Data;
using GastosAPI.Utilidades;

namespace GastosAPI.Repository.Implementacion
{
    public class TasaCambioRepositoty : ITasaCambioRepository
    {
        private readonly GastosDbContext _dbContext;
        //ServiceReference.Tipo_Cambio_BCNSoapClient tipo_cambio = null;
        private readonly ServiceReference.Tipo_Cambio_BCNSoapClient _tipo_Cambio_BCNSoapClient;


        public TasaCambioRepositoty(GastosDbContext dbContext, Tipo_Cambio_BCNSoapClient tipo_Cambio_BCNSoapClient)
        {
            _dbContext = dbContext;
            _tipo_Cambio_BCNSoapClient = tipo_Cambio_BCNSoapClient;
        }

        public async Task<IQueryable<TasaCambio>> Consultar(Expression<Func<TasaCambio, bool>> filtro = null)
        {
            try
            {
                IQueryable<TasaCambio> query = filtro == null ? _dbContext.TasaCambios : _dbContext.TasaCambios.Where(filtro);
                return query;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> InsetarMes(DateTime fecha)
        {
            try
            {

                TasaCambio oTasaCambio = new TasaCambio();

                DateTime fechaBusqueda = fecha.Date;

                int mes = fechaBusqueda.Month;
                int año = fechaBusqueda.Year;

                oTasaCambio = await _dbContext.TasaCambios.FirstOrDefaultAsync(x => x.Fecha.Value.Month == mes && x.Fecha.Value.Year == año);

                if (oTasaCambio != null)
                {
                    return false;
                    //return throw("No se encontró ninguna tasa de cambio para la fecha proporcionada.");
                }

                // Consumir el servicio de manera asíncrona
                var response = await _tipo_Cambio_BCNSoapClient.RecuperaTC_MesAsync(fecha.Year, fecha.Month);

                // Obtener el XML del resultado de la tarea
                var xmlElement = response.Body.RecuperaTC_MesResult;

                // Procesar el XML como lo estabas haciendo
                XmlNodeList xmlNodLista = xmlElement.GetElementsByTagName("Tc");
                DataTable dtTipoCambio = new DataTable();

                // Agregar las columnas al DataTable
                foreach (XmlNode Node in xmlNodLista.Item(0).ChildNodes)
                {
                    DataColumn Col = new DataColumn(Node.Name, typeof(string));
                    dtTipoCambio.Columns.Add(Col);
                }

                // Agregar la información al DataTable
                for (int IntVal = 0; IntVal < xmlNodLista.Count; IntVal++)
                {
                    DataRow dr = dtTipoCambio.NewRow();
                    for (int Col = 0; Col < dtTipoCambio.Columns.Count; Col++)
                    {
                        if ((xmlNodLista.Item(IntVal).ChildNodes[Col].InnerText) != null)
                        {
                            dr[Col] = xmlNodLista.Item(IntVal).ChildNodes[Col].InnerText;
                        }
                        else
                        {
                            dr[Col] = null;
                        }
                    }
                    dtTipoCambio.Rows.Add(dr);
                }

                foreach (DataRow fila in dtTipoCambio.Rows)
                {
                    TasaCambio tasaCambio = new TasaCambio
                    {
                        IdTasaCambio = Guid.NewGuid(),
                        FechaRegistro = DateTime.Now,
                        Fecha = Convert.ToDateTime(fila[0].ToString()),
                        TipoCambio = Convert.ToDouble(fila[1].ToString()),

                    };
                    _dbContext.Set<TasaCambio>().Add(tasaCambio);


                    //listaTasasCambio.Add(tasaCambio);
                }
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
                throw;
            }
        }

        public Task<TasaCambio> GetTasaCambio(DateTime fecha)
        {
            try
            {
                return _dbContext.TasaCambios.Where(x => x.Fecha.Value.Month == fecha.Month && x.Fecha.Value.Day == fecha.Day && x.Fecha.Value.Year == fecha.Year).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
