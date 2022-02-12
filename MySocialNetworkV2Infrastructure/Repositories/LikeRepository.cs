using MySocialNetworkV2Core.DTOs.CreationDTOs;
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
    public class LikeRepository : BaseRepository<Like>, ILikeRepository
    {
        public LikeRepository(MySocialNetworkContext context) : base(context) { }
        public bool CheckExistingLike(LikeCreationDTO like)
        {
            var collection = _entities as IQueryable<Like>;
            var likePost = collection.Where(x => x.IdPost == like.IdPost);
            var likeUser = collection.Where(x => x.IdUser == like.IdUser);
            if (likePost.Count() > 0 && likeUser.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task DeleteLike(LikeCreationDTO like)
        {
            var collection = _entities as IQueryable<Like>;
            var entity = collection.Where(x => x.IdPost == like.IdPost && x.IdUser == like.IdUser).FirstOrDefault();
            _entities.Remove(entity);
        }

        public IEnumerable<int> GetPostsLikedByUser(int idUser)
        {
            var collection = _entities as IQueryable<Like>;
            var UsersPosts = collection.Where(x => x.IdUser == idUser).Select(x=> x.IdPost);

            return UsersPosts;
        }
    }
}
