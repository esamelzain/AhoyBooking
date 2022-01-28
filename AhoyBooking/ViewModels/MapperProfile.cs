using AhoyBooking.Models;
using AhoyBooking.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhoyBooking
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<HotelViewModel, Hotel>()
                .ForMember(h => h.DateCreated, map => map.MapFrom(c => DateTime.Now))
                .ForMember(h => h.IsDeleted, map => map.MapFrom(c => false));

            CreateMap<BookVM, Book>()
                .ForMember(b => b.DateCreated, map => map.MapFrom(c => DateTime.Now))
                .ForMember(b => b.IsDeleted, map => map.MapFrom(c => false))
                .ForMember(b => b.IsConfirmed, map => map.MapFrom(c => false));

            CreateMap<PriceVM, RoomsPrice>()
                .ForMember(h => h.DateCreated, map => map.MapFrom(c => DateTime.Now))
               .ForMember(h => h.IsDeleted, map => map.MapFrom(c => false));
        }
    }
}
