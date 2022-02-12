using AutoMapper;
using MySocialNetworkV2Core.DTOs;
using MySocialNetworkV2Core.DTOs.CreationDTOs;
using MySocialNetworkV2Core.Entities;
using MySocialNetworkV2Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetworkV2Infrastructure.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => $"{src.User.Name} {src.User.LastName}"))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("MM/dd/yyyy HH:mm")))
                .ForMember(dest => dest.UserImage, opt => opt.MapFrom(src => src.User.ProfileImage))
                .ForMember(dest=> dest.Comments, opt=> opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Count));


            CreateMap<PostDTO, Post>();

            CreateMap<PostCreationDTO, Post>()
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser));
        }
    }
}
