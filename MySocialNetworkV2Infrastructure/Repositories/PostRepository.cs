using Microsoft.EntityFrameworkCore;
using MySocialNetworkV2Core.Entities;
using MySocialNetworkV2Core.Helpers;
using MySocialNetworkV2Core.Interfaces;
using MySocialNetworkV2Core.QueryFilters;
using MySocialNetworkV2Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(MySocialNetworkContext context) : base(context) { }

        public Post GetPostByIdSync(int id)
        {
            return _entities.Find(id);
        }

        public async Task<PagedList<Post>> GetPosts(PostQueryFilter postQueryFilter)
        {
            var collection = _entities as IQueryable<Post>;
            if (!string.IsNullOrWhiteSpace(postQueryFilter.SearchQuery))
            {
                postQueryFilter.SearchQuery = postQueryFilter.SearchQuery.Trim();
                collection = collection.Where(x => x.Description.Contains(postQueryFilter.SearchQuery) || x.User.Name.Contains(postQueryFilter.SearchQuery) || x.User.LastName.Contains(postQueryFilter.SearchQuery));
            }
            collection = collection.OrderByDescending(x => x.Date);
            return PagedList<Post>.Create(collection, postQueryFilter.PageNumber, postQueryFilter.PageSize);
        }

        public async Task<IEnumerable<Post>> GetPostsByUser(int iduser)
        {
            return await _entities.Where(x => x.IdUser == iduser).ToListAsync();
        }

    }
}
