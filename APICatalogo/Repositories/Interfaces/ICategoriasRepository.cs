using APICatalogo.Models;

namespace APICatalogo.Repositories.Interfaces;

public interface ICategoriasRepository
{
    List<Categoria> GetCategorias();
    Categoria GetCategoriaId(int id);
    List<Categoria> GetCategoriaEProdutos();
    List<Categoria> GetCategoriasProdutos(int id);
    Categoria Create(Categoria categoria);
    Categoria Update(Categoria categoria);
    Categoria Delete(int id);
}
