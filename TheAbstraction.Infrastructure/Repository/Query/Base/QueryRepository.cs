using TheAbstraction.Domain.Repositories.Query.Base;
using TheAbstraction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TheAbstraction.Infrastructure.Repository.Query.Base
{
    public class QueryRepository<T>(ApplicationDbContext context) : IQueryRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }
    }
}