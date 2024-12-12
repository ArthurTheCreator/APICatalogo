using APICatalogo.Models;

namespace APICatalogo.Repositories.Interfaces;

public interface ICategoriasRepository : IRepository<Categoria>
{
    List<Categoria> GetCategoriaEProdutos();
    List<Categoria> GetCategoriasProdutos(int id);
}
