using Microsoft.EntityFrameworkCore;
using MySocialNetworkV2Core.Entities;
using MySocialNetworkV2Core.Entities.CustomEntities;
using MySocialNetworkV2Core.Interfaces;
using MySocialNetworkV2Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly MySocialNetworkContext _context;
        protected DbSet<T> _entities;
        public BaseRepository(MySocialNetworkContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public void Insert(T entity)
        {
            _entities.Add(entity);
        }

        public async Task Update(T entity)
        {
            _entities.Update(entity);
        }

        public async Task Delete(int id)
        {
            T Entity = await GetById(id);
            _entities.Remove(Entity);

        }
    }
}
