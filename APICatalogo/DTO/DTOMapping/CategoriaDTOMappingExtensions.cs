using APICatalogo.Arguments.Categorias;
using APICatalogo.Models;
namespace APICatalogo.DTO.DTOMapping;

public static class CategoriaDTOMappingExtensions
{
    public static OutputCategoria? ToOuputCategoria(this Categoria categoria)
    {
        if(categoria is null)
            return null;
        return new OutputCategoria(categoria.CategoriaId, categoria.Nome, categoria.ImagemUrl);
    }
    public static Categoria? ToCategoria(this InputCreateCategoria categoria)
    {
        if(categoria is null) return null;
        return new Categoria(categoria.Nome, categoria.ImagemUrl, null);
    }
    public static Categoria? ToCategoria(this InputUpdateCategoria categoria)
    {
        if (categoria is null) return null;
        return new Categoria(categoria.Nome, categoria.ImagemUrl, null);
    }
    public static List<OutputCategoria> ToOutputCategoriaList(this List<Categoria> categoria)
    {
        if (categoria is null) return null;
        return categoria.Select(c => new OutputCategoria(c.CategoriaId, c.Nome, c.ImagemUrl)).ToList();
    }
}