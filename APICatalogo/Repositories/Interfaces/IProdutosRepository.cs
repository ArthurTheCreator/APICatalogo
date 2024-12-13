using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutosRepository : IRepository<Produto>
{
    List<Produto> GetProdutos(ProdutosParameters produtosParams);
}
