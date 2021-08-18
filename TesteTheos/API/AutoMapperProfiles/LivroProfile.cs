using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TesteTheos.API.Models;
using TesteTheos.Data;

namespace TesteTheos.API.AutoMapperProfiles
{
    public class LivroProfile : Profile
    {
        public LivroProfile()
        {
            CreateMap<Livro, LivroViewModel>();
            CreateMap<Livro, LivroDetalhesViewModel>();
            CreateMap<LivroModel, Livro>();
        }
    }
}
