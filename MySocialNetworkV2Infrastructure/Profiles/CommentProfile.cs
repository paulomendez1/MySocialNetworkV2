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
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => $"{src.User.Name} {src.User.LastName}"))
                .ForMember(dest => dest.UserImage, opt => opt.MapFrom(src => src.User.ProfileImage));


            CreateMap<CommentDTO, Comment>();

            CreateMap<CommentCreationDTO, Comment>()
              .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser));
        }
    }
}
