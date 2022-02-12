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
    [Route("api/post/{postId}/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetCommentsForPost")]
        public async Task<IActionResult> GetCommentsForPost(int postId)
        {
            if (await _unitOfWork.PostRepository.GetById(postId) ==null)
            {
                return NotFound();
            }
            var commentsForPost = await _unitOfWork.CommentRepository.GetCommentsByPost(postId);
            return Ok(_mapper.Map<IEnumerable<CommentDTO>>(commentsForPost));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentsForUser(int id)
        {
            var comments = await _unitOfWork.CommentRepository.GetCommentsByUser(id);
            var commentsDTO = _mapper.Map<IEnumerable<CommentDTO>>(comments);
            return Ok(commentsDTO.OrderByDescending(x => x.Date));
        }

        [HttpPost(Name = "CreateCommentForPost")]
        public async Task<IActionResult> CreateCommentForPost([FromBody] CommentCreationDTO comment)
        {
            if (comment == null)
            {
                throw new ArgumentException(nameof(comment));
            }
            var commentEntity = _mapper.Map<Comment>(comment);
            _unitOfWork.CommentRepository.Insert(commentEntity);
            await _unitOfWork.SaveChangesAsync();
            var commentToReturn = _mapper.Map<CommentDTO>(commentEntity);
            return CreatedAtRoute("", new { id = commentEntity.Id }, commentToReturn);
        }

    }
}
