
using HotelManagement_BusinessObject.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace HotelManagement_DAO
{
    public class GenericDAO<T> where T : class
    {
        private FUMiniHotelManagementContext _context;
        private DbSet<T> _dbSet;
        private static GenericDAO<T> _instance;

        public static GenericDAO<T> Instance
        {
            get
            {
                //if (_instance == null)
                _instance = new GenericDAO<T>();
                return _instance;
            }
            set { }

        }
        public GenericDAO()
        {
            _context = new FUMiniHotelManagementContext();
            _dbSet = _context.Set<T>();
        }
        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) query = orderBy(query);
            var entity =  await query.AsNoTracking().FirstOrDefaultAsync();
          
            return entity;
        }
        public async Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) query =  orderBy(query);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<bool> InsertAsync(T entity)
        {
            if (entity == null) return false;
            await _dbSet.AddAsync(entity);
            return _context.SaveChanges() > 0;
        }
        public bool Update(T entity)
        {
            
            _dbSet.Update(entity);
            var result =  _context.SaveChanges() > 0;
            return result;
        }

        public bool Delete(T entity)
        {
            _dbSet.Remove(entity);
            var result = _context.SaveChanges() > 0;
            return result;
        }


    }
     
}
