using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
namespace APICatalogo.Repositories.Repositories;

public class ProdutosRepository : Repository<Produto>, IProdutosRepository
{
    public ProdutosRepository(AppDbContext context) : base(context)
    {
    }
}