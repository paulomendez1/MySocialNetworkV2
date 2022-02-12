using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySocialNetworkV2Core.DTOs;
using MySocialNetworkV2Core.DTOs.CreationDTOs;
using MySocialNetworkV2Core.Entities;
using MySocialNetworkV2Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySocialNetworkV2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LikeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost(Name = "CreateLike")]
        public async Task<IActionResult> CreateLike(LikeCreationDTO like)
        {
            if (like == null)
            {
                throw new ArgumentException(nameof(like));
            }
            if (GetLike(like))
            {
                return Ok(false);
            }
            else
            {
                var likeEntity = _mapper.Map<Like>(like);
                _unitOfWork.LikeRepository.Insert(likeEntity);
                await _unitOfWork.SaveChangesAsync();
                return CreatedAtRoute("", new { id = likeEntity.Id }, likeEntity);
            }
        }

        [HttpDelete(Name = "DeleteLike")]
        public async Task<IActionResult> DeleteLike([FromQuery] LikeCreationDTO like)
        {
            if (!_unitOfWork.LikeRepository.CheckExistingLike(like))
            {
                return NotFound();
            }
            await _unitOfWork.LikeRepository.DeleteLike(like);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        private bool GetLike(LikeCreationDTO like)
        {
            var existingLike = _unitOfWork.LikeRepository.CheckExistingLike(like);
            return existingLike;
        }

    }
}
