using Microsoft.EntityFrameworkCore;
using MySocialNetworkV2Core.Entities;
using MySocialNetworkV2Core.Interfaces;
using MySocialNetworkV2Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(MySocialNetworkContext context) : base(context) { }
        public async Task<IEnumerable<Comment>> GetCommentsByPost(int idPost)
        {
            var collection = _entities as IQueryable<Comment>;
            collection = collection.Where(x => x.IdPost == idPost);

            return await collection.ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUser(int idUser)
        {
            var collection = _entities as IQueryable<Comment>;
            collection = collection.Where(x => x.IdUser == idUser);

            return await collection.ToListAsync();
        }
    }
}
