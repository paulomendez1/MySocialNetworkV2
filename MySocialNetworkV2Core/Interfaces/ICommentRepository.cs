using MySocialNetworkV2Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Core.Interfaces
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByPost(int idPost);

        Task<IEnumerable<Comment>> GetCommentsByUser(int idUser);
    }
}
