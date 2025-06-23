
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MyBlogAppClient.Models;

namespace MyBlogAppClient.Controllers
{
    public class PostController : Controller
    {


        public async Task<IActionResult> Posts()
        {
            var posts = new List<PostDTO>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5261/api/posts/list"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        posts = JsonSerializer.Deserialize<List<PostDTO>>(apiResponse, options);

                    }
                    else
                    {

                    }
                }
            }

            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {
            PostDetailDTO postDetail = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5261/api/posts/details/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        postDetail = JsonSerializer.Deserialize<PostDetailDTO>(apiResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }

            return View(postDetail);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        

        public IActionResult MyPost()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EditPostDTO editpost = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5261/api/posts/details/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var detailDto = JsonSerializer.Deserialize<PostDetailDTO>(apiResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        editpost = new EditPostDTO
                        {
                            PostId = detailDto.PostId,
                            Title = detailDto.Title,
                            Content = detailDto.Content,
                            Url = detailDto.Url,
                            IsActive = detailDto.IsActive,
                            Description = detailDto.Description
                            
                        };
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }

            return View(editpost);
        }
        
    }
}