using AutoMapper;
using MyStore.Catalago.Application.ViewModels;
using MyStore.Catalago.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Catalago.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Produto, ProdutoDto>()
                .ForMember(d => d.Largura, o => o.MapFrom(s => s.Dimensoes.Largura))
                .ForMember(d => d.Altura, o => o.MapFrom(s => s.Dimensoes.Altura))
                .ForMember(d => d.Profundidade, o => o.MapFrom( s => s.Dimensoes.Profundidade));
            CreateMap<Categoria, CategoriaDto>();
        }
    }
}
