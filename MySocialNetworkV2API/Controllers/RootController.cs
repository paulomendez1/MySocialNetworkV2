using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySocialNetworkV2Core.Entities.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySocialNetworkV2API.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot()
        {
            var links = new List<LinkDTO>();

            links.Add(new LinkDTO(Url.Link("GetRoot", new { }), "self", "GET"));

            links.Add(new LinkDTO(Url.Link("GetPosts", new { }), "posts", "GET"));

            links.Add(new LinkDTO(Url.Link("CreatePost", new { }), "create_posts", "POST"));

            return Ok(links);
        }
    }
}
