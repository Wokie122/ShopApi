using ShopApi.Entities;
using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ShopApi
{
    public class SklepMappingProfile : Profile
    {
        public SklepMappingProfile()
        {

            CreateMap<CreateArtykulDto, Artykul>();

        }
    }
}
