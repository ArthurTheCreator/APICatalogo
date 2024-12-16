using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Pagination.Filter;

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutosRepository : IRepository<Produto>
{
    Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParams);
    Task<PagedList<Produto>> GetProdutosFiltroPreco(ProdutosFIltroPreco produtosFIltroPreco);
}
