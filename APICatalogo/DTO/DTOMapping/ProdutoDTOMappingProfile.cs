using APICatalogo.Arguments.Categorias;
using APICatalogo.Arguments.Produtos;
using APICatalogo.Models;
using AutoMapper;

namespace APICatalogo.DTO.DTOMapping
{
    public class ProdutoDTOMappingProfile : Profile
    {
        public ProdutoDTOMappingProfile()
        {
            CreateMap<Produto, OutputProduto>().ReverseMap();
            CreateMap<InputCreateProduto, Produto>().ReverseMap();
            CreateMap<InputUpdateProduto, Produto>().ReverseMap();
            CreateMap<Categoria, OutputCategoria>().ReverseMap();
            CreateMap<InputCreateCategoria, Categoria>().ReverseMap();
            CreateMap<InputUpdateCategoria, Categoria>().ReverseMap();
        }
    }
}
