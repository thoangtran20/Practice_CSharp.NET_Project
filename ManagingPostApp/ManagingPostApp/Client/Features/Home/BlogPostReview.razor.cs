using ManagingPostApp.Shared;
using Microsoft.AspNetCore.Components;

namespace ManagingPostApp.Client.Features.Home
{
    public class BlogPostReviewModel : ComponentBase
    {
        [Parameter] public BlogPost blogPost { get; set; }
    }
}
