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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MySocialNetworkContext _context;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;
        public UnitOfWork(MySocialNetworkContext context)
        {
            _context = context;
        }
        public IPostRepository PostRepository => _postRepository ?? new PostRepository(_context);

        public ICommentRepository CommentRepository => _commentRepository ?? new CommentRepository(_context);
        public ILikeRepository LikeRepository => _likeRepository ?? new LikeRepository(_context);


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
