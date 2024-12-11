using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories.Repositories;

public class ProdutosRepository : IProdutosRepository
{
    private readonly AppDbContext _context;

    public ProdutosRepository(AppDbContext context)
    {
        _context = context;
    }

    public Produto Create(Produto produto)
    {
        if (produto is null)
        {
            throw new ArgumentNullException(nameof(produto));
        }
        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return produto;
    }

    public Produto Delete(int id)
    {
        var produtoDeletado = _context.Produtos.Find(id);
        if (produtoDeletado is null)
        {
            throw new ArgumentNullException(" >>> Produto Não encontrado <<<");
        }
        _context.Produtos.Remove(produtoDeletado);
        _context.SaveChanges();
        return produtoDeletado;
    }

    public Produto GetProduto(int id)
    {
        var produto = _context.Produtos.Find(id);
        if (produto is null)
        {
            throw new ArgumentNullException(nameof(produto));
        }
        return produto;
        
    }

    public List<Produto> GetProdutos()
    {
        var produtos = _context.Produtos.ToList();
        if (produtos is null)
        {
            throw new ArgumentNullException(nameof(produtos));
        }
        return produtos;
    }

    public Produto Update(Produto produto)
    {
        
        if (produto is null)
        {
            throw new ArgumentNullException(nameof(produto));
        }
        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();
        return produto;
    }
}
