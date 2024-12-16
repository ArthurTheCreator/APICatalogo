using APICatalogo.Context;
using APICatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.Repositories.Repositories;

public class Repository<T>: IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAll()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> Get(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        //_context.SaveChanges();
        return entity;
    }

    public T Update(T entity)
    {
        _context.Set<T>().Entry(entity).State = EntityState.Modified;
        //_context.SaveChanges(); 
        return entity;
    }

    public bool Delete(int id)
    {
        var dele = _context.Set<T>().Find(id);
        if (dele is null)
        {
            return false;
        }
        _context.Remove(dele);
        //_context.SaveChanges();
        return true;
    }
}
