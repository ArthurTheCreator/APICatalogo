using APICatalogo.Context;
using APICatalogo.Repositories.Interfaces;
using APICatalogo.Repositories.Repositories;

namespace APICatalogo.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private IProdutosRepository? _produtoRepo;
    private ICategoriasRepository? _categoriaRepo;
    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public IProdutosRepository ProdutosRepository
    {
        get
        {
            return _produtoRepo = _produtoRepo ?? new ProdutosRepository(_context); // Se for null cria um novo ProdutoosReposutory
        }
    }
    public ICategoriasRepository CategoriasRepository
    {
        get
        {
            return _categoriaRepo = _categoriaRepo ?? new CategoriasRepository(_context);
        }
    }
    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
    public void Dispode()
    {
        _context.Dispose();
    }
}
