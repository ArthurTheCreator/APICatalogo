using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace APICatalogo.Repositories.Repositories;

public class CategoriasRepository : Repository<Categoria>, ICategoriasRepository
{
    private readonly AppDbContext _context;

    public CategoriasRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public List<Categoria> GetCategoriaEProdutos()
    {
        return _context.Set<Categoria>().Include(p => p.Produtos).ToList() ;
    }

    public List<Categoria> GetCategoriasProdutos(int id)
    {
        return _context.Set<Categoria>().Include(p => p.Produtos).Where(p => p.CategoriaId == id).ToList();
    }
}