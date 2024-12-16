using APICatalogo.Models;

namespace APICatalogo.Repositories.Interfaces;

public interface ICategoriasRepository : IRepository<Categoria>
{
    Task<List<Categoria>> GetCategoriaEProdutos();
    Task<List<Categoria>> GetCategoriasProdutos(int id);
}
