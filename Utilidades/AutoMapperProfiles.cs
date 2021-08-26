using AutoMapper;
using fotoTeca.Models.IMG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Mapeo desde y hacia 
            CreateMap<IMGDTO, IMGdataTable>();
        }
    }
}
