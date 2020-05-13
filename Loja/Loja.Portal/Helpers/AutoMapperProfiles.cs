using AutoMapper;
using Loja.Domain.Db;
using Loja.Repository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Portal.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Produto, ProdutoDto>()
                .ReverseMap();           
                
        }
    }
}
