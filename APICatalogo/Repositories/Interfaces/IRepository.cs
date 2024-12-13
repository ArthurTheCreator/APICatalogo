using System.Linq.Expressions;

namespace APICatalogo.Repositories.Interfaces;

public interface IRepository<T>
{
    Task<List<T>> GetAll();
    Task<T?> Get(Expression<Func<T, bool>> predicate); // Aceita todos os tipos de ID's
    T Create(T entity);
    T Update(T entity);
    bool Delete(int id);
}