using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Pagination.Filter;

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutosRepository : IRepository<Produto>
{
    PagedList<Produto> GetProdutos(ProdutosParameters produtosParams);
    PagedList<Produto> GetProdutosFiltroPreco(ProdutosFIltroPreco produtosFIltroPreco);
}
