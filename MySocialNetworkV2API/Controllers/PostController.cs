using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Net.Http.Headers;
using MySocialNetworkV2Core.DTOs;
using MySocialNetworkV2Core.DTOs.CreationDTOs;
using MySocialNetworkV2Core.Entities;
using MySocialNetworkV2Core.Entities.CustomEntities;
using MySocialNetworkV2Core.Helpers;
using MySocialNetworkV2Core.Interfaces;
using MySocialNetworkV2Core.QueryFilters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MySocialNetworkV2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public PostController(IUnitOfWork unitOfWork, IMapper mapper, IPropertyCheckerService propertyCheckerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _propertyCheckerService = propertyCheckerService;
        }

        [Produces("application/json",
                  "application/vnd.paulo.hateoas+json")]
        [HttpGet(Name = "GetPosts")]
        public async Task<IActionResult> GetPosts([FromQuery] PostQueryFilter postQueryFilter, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }
            if (!_propertyCheckerService.TypeHasProperties<PostDTO>(postQueryFilter.Fields))
            {
                return BadRequest();
            }
            var posts = await _unitOfWork.PostRepository.GetPosts(postQueryFilter);

            var paginationMetadata = new
            {
                totalCount = posts.TotalCount,
                pageSize = posts.PageSize,
                currentPage = posts.CurrentPage,
                totalPages = posts.TotalPages
            };

            HttpContext.Response.Headers.Append("x-pagination", JsonConvert.SerializeObject(paginationMetadata));

            if (parsedMediaType.MediaType == "application/vnd.paulo.hateoas+json")
            {
                var links = CreateLinksForPosts(postQueryFilter,
                 posts.HasNext,
                 posts.HasPrevious);

                var shapedPosts = _mapper.Map<IEnumerable<PostDTO>>(posts).ShapeData(postQueryFilter.Fields);

                var shapedPostsWithLinks = shapedPosts.Select(post =>
                {
                    var postAsDictionary = post as IDictionary<string, object>;
                    var authorLinks = CreateLinksForPost((int)postAsDictionary["Id"], null);
                    postAsDictionary.Add("links", authorLinks);
                    return postAsDictionary;
                });

                var linkedCollectionResource = new
                {
                    value = shapedPostsWithLinks,
                    links
                };

                return Ok(linkedCollectionResource);
            }
            var postsToReturn = _mapper.Map<IEnumerable<PostDTO>>(posts).ShapeData(postQueryFilter.Fields);
            return Ok(postsToReturn);
        }

        [HttpGet("profile/{id}/posts", Name = "GetPostsByUser")]
        public async Task<IActionResult> GetPostsByUser(int id)
        {
            await WriteOutIdentityInformation();
            var posts = await _unitOfWork.PostRepository.GetPostsByUser(id);
            var postsDTO = _mapper.Map<IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO.OrderByDescending(x => x.Date));
        }

        [HttpGet("profile/{id}/likes", Name = "GetPostsLikedByUser")]
        public IActionResult GetPostsLikedByUser(int id)
        {
            var collection = _unitOfWork.LikeRepository.GetPostsLikedByUser(id);
            var postCollection = new List<Post>();
            foreach (var item in collection)
            {
                var post = _unitOfWork.PostRepository.GetPostByIdSync(item);
                postCollection.Add(post);
            }
            IEnumerable<Post> posts = postCollection;
            var postsDTO = _mapper.Map<IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }

        [Produces("application/json",
             "application/vnd.paulo.hateoas+json")]
        [HttpGet("{postId}", Name = "GetPost")]
        public async Task<IActionResult> GetPost(int postId, string fields, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }
            if (!_propertyCheckerService.TypeHasProperties<PostDTO>(fields))
            {
                return BadRequest();
            }
            var post = await _unitOfWork.PostRepository.GetById(postId);
            if (post==null)
            {
                return NotFound();
            }

            if (parsedMediaType.MediaType == "application/vnd.paulo.hateoas+json")
            {
                var links = CreateLinksForPost(postId, fields);
                var linkedResourceToReturn = _mapper.Map<PostDTO>(post).ShapeData(fields) as IDictionary<string, object>;
                linkedResourceToReturn.Add("links", links);
                return Ok(linkedResourceToReturn);
            }

            return Ok(_mapper.Map<PostDTO>(post).ShapeData(fields));
          
        }

        [HttpPost(Name ="CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody]PostCreationDTO post)
        {
            if (post== null)
            {
                throw new ArgumentException(nameof(post));
            }
            var postEntity = _mapper.Map<Post>(post);
            _unitOfWork.PostRepository.Insert(postEntity);
            await _unitOfWork.SaveChangesAsync();
            var postToReturn = _mapper.Map<PostDTO>(postEntity);

            var links = CreateLinksForPost(postToReturn.Id, null);
            var linkedResourceToReturn = postToReturn.ShapeData(null) as IDictionary<string, object>;
            linkedResourceToReturn.Add("links", links);

            return CreatedAtRoute("GetPost", new { id= linkedResourceToReturn["Id"]}, linkedResourceToReturn);
        }

        [HttpDelete("{postId}", Name ="DeletePost")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            if (_unitOfWork.PostRepository.GetById(postId) == null)
            {
                return NotFound();
            }
            await _unitOfWork.PostRepository.Delete(postId);
            return NoContent();
        }

        private string CreatePostResourceUri(PostQueryFilter postQueryFilter, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetPosts",
                        new
                        {
                            fields = postQueryFilter.Fields,
                            pageNumber = postQueryFilter.PageNumber - 1,
                            pageSize = postQueryFilter.PageSize,
                            searchQuery = postQueryFilter.SearchQuery
                        }); ;
                case ResourceUriType.NextPage:
                    return Url.Link("GetPosts",
                        new
                        {
                            fields = postQueryFilter.Fields,
                            pageNumber = postQueryFilter.PageNumber + 1,
                            pageSize = postQueryFilter.PageSize,
                            searchQuery = postQueryFilter.SearchQuery
                        });
                case ResourceUriType.Current:
                default:
                    return Url.Link("GetPosts",
                         new
                         {
                             fields = postQueryFilter.Fields,
                             pageNumber = postQueryFilter.PageNumber,
                             pageSize = postQueryFilter.PageSize,
                             searchQuery = postQueryFilter.SearchQuery
                         });
            }
        }

        private IEnumerable<LinkDTO> CreateLinksForPost(int postId, string fields)
        {
            var links = new List<LinkDTO>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(new LinkDTO(Url.Link("GetPost", new { postId }),
                    "self", "GET"));
            }
            else
            {
                links.Add(new LinkDTO(Url.Link("GetPost", new { postId, fields }),
                  "self", "GET"));
            }

            links.Add(new LinkDTO(Url.Link("DeletePost", new { postId = postId}),
                  "delete_post", "DELETE"));

            links.Add(new LinkDTO(Url.Link("CreateCommentForPost", new { postId }),
                  "create_comment_for_post", "POST"));

            links.Add(new LinkDTO(Url.Link("GetCommentsForPost", new { postId }),
               "comments", "GET"));

            return links;
        }

        private IEnumerable<LinkDTO> CreateLinksForPosts(
            PostQueryFilter postQueryFilter,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDTO>();

            // self 
            links.Add(
               new LinkDTO(CreatePostResourceUri(
                   postQueryFilter, ResourceUriType.Current)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDTO(CreatePostResourceUri(
                      postQueryFilter, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDTO(CreatePostResourceUri(
                        postQueryFilter, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }

        private async Task WriteOutIdentityInformation()
        {
            // get the saved identity token
            var identityToken = await HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            // write it out
            Debug.WriteLine($"Identity token: {identityToken}");

            // write out the user claims
            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }

    }
}
