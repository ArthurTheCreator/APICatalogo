namespace APICatalogo.Repositories.Interfaces;

public interface IRepository<T>
{
    List<T> GetAll();
    T Get(int id);
    T Create(T entity);
    T Update(T entity);
    bool Delete(T entity);
    bool Delete(int id);
}
