using AutoMapper;
using GastosAPI.DTO;
using GastosAPI.Models;

namespace GastosAPI.Utilidades
{
    public class AutoMapper : Profile
    {   
        public AutoMapper() 
        {
            //Usuario
            CreateMap<Usuario, UsuarioDTO>()
            .ForMember(destino => destino.NombreCompleto,
            opt => opt.MapFrom(origen => origen.PrimerNombre + " " + origen.SegundoNombre));

            CreateMap<Usuario, TasaCambioDTO>().ReverseMap();

            //Register
            CreateMap<Usuario, RegisterDTO>().ReverseMap();

            //Balance
            CreateMap<Balance, BalanceDTO>().ReverseMap();

            //Categoria
            CreateMap<Categoria,CategoriaDTO>().ReverseMap();

            //Lugar
            CreateMap<Lugar, LugarDTO>().ReverseMap();

            //Metodo Pago
            CreateMap<MetodoPago, MetodoPagoDTO>().ReverseMap();

            //TipoCambio
            CreateMap<TasaCambio, TasaCambioDTO>().ReverseMap();

            //Transaccion
            CreateMap<Transaccion, TransaccionDTO>()
                .ForMember(destino => destino.NombreCategoria,
                opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.NombreCategoria))
                .ForMember(destino => destino.NombreLugar,
                opt => opt.MapFrom(origen => origen.IdLugarNavigation.NombreLugar))
                 .ForMember(destino => destino.NombreMetodoPago,
                opt => opt.MapFrom(origen => origen.IdMetodoPagoNavigation.Descripcion));

            CreateMap<TransaccionDTO, Transaccion>()
                .ForMember(destino => destino.IdCategoriaNavigation, opt => opt.Ignore())
                .ForMember(destino => destino.IdMetodoPagoNavigation, opt => opt.Ignore())
                .ForMember(destino => destino.IdLugarNavigation, opt => opt.Ignore()

                );
        }
    }
}
