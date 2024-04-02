using ManagingPostApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ManagingPostApp.Client.Features.AddPost
{
    public class AddPostModel : ComponentBase
    {
        [Inject] private HttpClient _httpClient { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        protected string Post { get; set; }
        protected string Title { get; set; }
        protected string ErrorMessage { get; set; }


        public async Task SavePost()
        {
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Post))
            {
                ErrorMessage = "Please enter both title and post.";
                return;
            }

            var newPost = new BlogPost()
            {
                Title = Title,
                Author = "Joe Bloggs",
                Post = Post,
                Posted = DateTime.Now
            };
            var response = await _httpClient.PostAsJsonAsync<BlogPost>(Urls.AddBlogPost, newPost);

            if (response.IsSuccessStatusCode)
            {
                var savedPost = await response.Content.ReadFromJsonAsync<BlogPost>();

                if (savedPost != null)
                {
                    _navigationManager.NavigateTo($"viewpost/{savedPost.Id}");
                }
            }
        }
    }
}
    