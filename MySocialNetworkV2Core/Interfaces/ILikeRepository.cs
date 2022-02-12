using MySocialNetworkV2Core.DTOs.CreationDTOs;
using MySocialNetworkV2Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Core.Interfaces
{
    public interface ILikeRepository : IBaseRepository<Like>
    {
        bool CheckExistingLike(LikeCreationDTO like);

        Task DeleteLike(LikeCreationDTO like);

        IEnumerable<int> GetPostsLikedByUser(int idUser);
    }
}
