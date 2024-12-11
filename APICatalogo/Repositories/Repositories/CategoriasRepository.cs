using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;

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
    public Categoria GetCategoria(int id)
    {
        return _context.Categorias.Find(id);
    }
    public OutputCategoria Create(Categoria categoria)
    {
        return
    }

    public Categoria Update(Categoria categoria)
    {
        throw new NotImplementedException();
    }

    public Categoria Delete(int id)
    {
        throw new NotImplementedException();
    }
}
