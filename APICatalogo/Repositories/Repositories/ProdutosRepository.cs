using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Pagination.Filter;
using APICatalogo.Repositories.Interfaces;

namespace APICatalogo.Repositories.Repositories;

public class ProdutosRepository : Repository<Produto>, IProdutosRepository
{
    public ProdutosRepository(AppDbContext context) : base(context)
    {
    }

    public PagedList<Produto> GetProdutos(ProdutosParameters produtosParams)
    {
        var produtos = GetAll()
            .OrderBy(p => p.Nome).AsQueryable();
        var produtosORdenados = PagedList<Produto>.ToPagedList(produtos, produtosParams.PageNumber, produtosParams.PageSize);
        return produtosORdenados;
            
    }

    public PagedList<Produto> GetProdutosFiltroPreco(ProdutosFIltroPreco produtosFIltroPreco)
    {
        var produtos = GetAll().AsQueryable();
        if (produtosFIltroPreco.Preco.HasValue && !string.IsNullOrEmpty(produtosFIltroPreco.PrecoCriterio))
        {
            if (produtosFIltroPreco.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco > produtosFIltroPreco.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFIltroPreco.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco > produtosFIltroPreco.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFIltroPreco.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco > produtosFIltroPreco.Preco.Value).OrderBy(p => p.Preco);
            }    
        }
        var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtosFIltroPreco.PageNumber, produtosFIltroPreco.PageSize);
        return produtosFiltrados;
    }
}