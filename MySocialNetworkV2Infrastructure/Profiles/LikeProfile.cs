using AutoMapper;
using MySocialNetworkV2Core.DTOs;
using MySocialNetworkV2Core.DTOs.CreationDTOs;
using MySocialNetworkV2Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Infrastructure.Profiles
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<Like, LikeDTO>();

            CreateMap<LikeDTO, Like>();

            CreateMap<LikeCreationDTO, Like>()
                        .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser))
                        .ForMember(dest => dest.IdPost, opt => opt.MapFrom(src => src.IdPost));

        }
    }
}
