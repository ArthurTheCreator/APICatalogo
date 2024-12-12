using APICatalogo.Repositories.Interfaces;

namespace APICatalogo.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    IProdutosRepository ProdutosRepository { get; }
    ICategoriasRepository CategoriasRepository { get; }

    void Commit();
}
