using TheAbstraction.Domain.Repositories.Command.Base;
using TheAbstraction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAbstraction.Infrastructure.Repository.Command.Base
{
    // Generic command repository class
    public class CommandRepository<T>(ApplicationDbContext context) : ICommandRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context = context;

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }


    }
}
