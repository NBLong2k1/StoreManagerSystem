using AutoMapper;
using BusinessObject.Model.Entities;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStoreWebAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Product, listCartDTO>();
           
            CreateMap<listCartDTO, Product>();


        }
            
    }
}
