using MySocialNetworkV2Core.Entities;
using MySocialNetworkV2Core.Helpers;
using MySocialNetworkV2Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Core.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByUser(int id);

        Task<PagedList<Post>> GetPosts(PostQueryFilter postQueryFilter);

        Post GetPostByIdSync(int id);
    }
}
