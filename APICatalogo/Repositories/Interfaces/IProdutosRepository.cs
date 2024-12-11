using APICatalogo.Models;

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutosRepository
{
    List<Produto> GetProdutos();
    Produto GetProduto(int id);
    Produto Create(Produto produto);
    Produto Update(Produto produto);
    Produto Delete(int id);
}
