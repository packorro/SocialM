using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Repositories;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            //Sin Injeccion de Dependencias
            //var post = new PostRepository().GetPosts();

            //Con Injeccion de Dependencia
            var posts = await _postService.GetPosts();
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            //Sin Injeccion de Dependencias
            //var post = new PostRepository().GetPosts();

            //Con Injeccion de Dependencia
            var post = await _postService.GetPost(id);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto )
        {
            //Sin Injeccion de Dependencias
            //var post = new PostRepository().GetPosts();

            //Con Injeccion de Dependencia
            var post = _mapper.Map<Post>(postDto);

            await _postService.InsertPost(post);

            postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPut]

        public async Task<IActionResult> Put(int id , PostDto postDto)
        {
            //Sin Injeccion de Dependencias
            //var post = new PostRepository().GetPosts();

            //Con Injeccion de Dependencia
            var post = _mapper.Map<Post>(postDto);
            post.PostId = id;

            var result = await _postService.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            //await _postRepository.InsertPost(post);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postService.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }


    }
}
