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

    public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParams)
    {
        var produtos = await GetAll();
        var produtosOrdenados = produtos
            .OrderBy(p => p.Nome).AsQueryable();
        var resultado = PagedList<Produto>.ToPagedList(produtosOrdenados, produtosParams.PageNumber, produtosParams.PageSize);
        return resultado;
            
    }

    public async Task<PagedList<Produto>> GetProdutosFiltroPreco(ProdutosFIltroPreco produtosFIltroPreco)
    {
        var produtos = await GetAll();

        if (produtosFIltroPreco.Preco.HasValue && !string.IsNullOrEmpty(produtosFIltroPreco.PrecoCriterio))
        {
            if (produtosFIltroPreco.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                var teste = produtos.Where(p => p.Preco > produtosFIltroPreco.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFIltroPreco.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                var teste = produtos.Where(p => p.Preco > produtosFIltroPreco.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFIltroPreco.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                var teste = produtos.Where(p => p.Preco > produtosFIltroPreco.Preco.Value).OrderBy(p => p.Preco);
            }    
        }
        var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos.AsQueryable(), produtosFIltroPreco.PageNumber, produtosFIltroPreco.PageSize);
        return produtosFiltrados;
    }
}