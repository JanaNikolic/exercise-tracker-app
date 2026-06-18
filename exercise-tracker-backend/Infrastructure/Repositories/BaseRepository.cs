using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

public class BaseRepository<T> where T : class
{
    protected readonly DbSet<T> _table;
    protected readonly DbContext _context;

    internal BaseRepository(DbContext context)
    {
        _context = context;
        _table = context.Set<T>();
    }

    protected async Task<EntityEntry<T>> AddAsync(T entity)
    {
        var newEntity = await _table.AddAsync(entity);
        await SaveChangesAsync();
        return newEntity;
    }

    protected async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    protected async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) => await _table.AsNoTracking().FirstOrDefaultAsync(predicate);

    protected async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => await _table.AnyAsync(predicate);

    protected async Task UpdateAsync(T entity)
    {
        _table.Update(entity);
        await SaveChangesAsync();
    }

    protected async Task<T?> FindAsync(long id) => await _table.FindAsync(id);

    protected async Task<IEnumerable<T>> FindAllWhereAsync(Expression<Func<T, bool>> predicate) => await _table.AsNoTracking().Where(predicate).ToListAsync();

    protected async Task Remove(T entity)
    {
        _table.Remove(entity);
        await SaveChangesAsync();
    }

    protected async Task<IEnumerable<T>> GetPagedListAsync(Expression<Func<T, bool>> filter,
                                                    int page,
                                                    int size) => await _table.AsNoTracking().Where(filter).Skip((page - 1) * size).Take(size).ToListAsync();

}