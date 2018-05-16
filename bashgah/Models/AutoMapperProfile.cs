using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace bashgah.Models
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<viewmodel,customer>();
            CreateMap<viewmodel, customer>();
            CreateMap<customer, Register>()
                .ForMember(dest => dest.CustomerId,
                opt => opt.MapFrom(src => src.Id));

        }

    }
}