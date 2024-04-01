using ManagingPostApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ManagingPostApp.Client.Features.ViewPost
{
    public class ViewPostModel : ComponentBase
    {
        [Inject] private HttpClient _httpClient {  get; set; }
        [Parameter] public string PostId { get; set; }
        protected BlogPost blogPost {  get; set; } = new BlogPost();
        protected override async Task OnInitializedAsync()
        {
            await LoadBlogPost();
        }
        private async Task LoadBlogPost()
        {
            blogPost = await _httpClient.GetFromJsonAsync<BlogPost>(Urls.BlogPost.Replace("{id}", PostId));
        }
    }
}
