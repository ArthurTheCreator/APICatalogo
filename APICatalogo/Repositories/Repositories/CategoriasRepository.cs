using APICatalogo.Arguments.Categorias;
using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories.Repositories;

public class CategoriasRepository : ICategoriasRepository
{
    private readonly AppDbContext _context;

    public CategoriasRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<Categoria> GetCategorias()
    {
        return _context.Categorias.ToList();
    }
    public Categoria GetCategoriaId(int id)
    {
        return _context.Categorias.Find(id);
    }
    public Categoria Create(Categoria categoria)
    {
        if (categoria is null)
        {
            throw new ArgumentException(nameof(categoria));
        }
        _context.Categorias.Add(categoria);
        _context.SaveChanges();
        return categoria;
    }

    public Categoria Update(Categoria categoria)
    {
        if (categoria is null)
        {
            throw new ArgumentException(nameof(categoria));
        }
        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();
        return categoria;
    }

    public Categoria Delete(int id)
    {
        var categoriaDeletada = _context.Categorias.Find(id);
        if (categoriaDeletada is null)
        {
            throw new ArgumentException($" >>> NÃO foi encontrada nenhuma categoria com id igual a {id} <<<");
        }
        _context.Categorias.Remove(categoriaDeletada);
        _context.SaveChanges();
        return categoriaDeletada;
    }

    public List<Categoria> GetCategoriaEProdutos()
    {
        return _context.Categorias.Include(p => p.Produtos).ToList();
    }

    public List<Categoria> GetCategoriasProdutos(int id)
    {
        return _context.Categorias.Include(p => p.Produtos).Where(p => p.CategoriaId == id).ToList();
    }
}
