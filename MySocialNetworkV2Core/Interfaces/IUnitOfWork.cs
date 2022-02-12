using MySocialNetworkV2Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        ICommentRepository CommentRepository { get; }
        ILikeRepository LikeRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
