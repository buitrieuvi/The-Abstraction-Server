using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheAbstraction.Domain.Repositories.Command.Base;
using TheAbstraction.Infrastructure.Data;

namespace TheAbstraction.Infrastructure.Repository.Command.Base
{
    // Generic command repository class
    public class CommandRepository<T>(ApplicationDbContext context) : ICommandRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context = context;

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
