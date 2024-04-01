using ManagingPostApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ManagingPostApp.Server.Controllers
{
    public class BlogPostsController : Controller
    {
        private List<BlogPost> _blogPosts { get; set; } = new List<BlogPost>
        {
            new BlogPost
            {
                Id = 1,
                Title = "If only C# worked in the browser",
                Post = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque id fermentum libero. Quisque mattis libero tortor, ac pharetra nunc imperdiet eu. Nulla commodo elit magna, eu suscipit tellus interdum nec. Cras pulvinar aliquam eros vitae posuere. Aliquam sit amet porta lacus. Donec vehicula neque mi, tincidunt convallis neque dignissim congue. Praesent at sollicitudin odio. Cras fermentum sapien ut neque volutpat, et pretium lorem cursus. Integer sodales nunc ut tellus accumsan interdum. Pellentesque faucibus, elit ac dignissim egestas, dui urna consectetur lacus, nec pellentesque massa metus eu eros.",
                Author = "Joe Bloggs",
                Posted = DateTime.Now.AddDays(-30)            
            },
             new BlogPost {
                Id = 2,
                Title = "400th JS Framework released",
                Post = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque id fermentum libero. Quisque mattis libero tortor, ac pharetra nunc imperdiet eu. Nulla commodo elit magna, eu suscipit tellus interdum nec. Cras pulvinar aliquam eros vitae posuere. Aliquam sit amet porta lacus. Donec vehicula neque mi, tincidunt convallis neque dignissim congue. Praesent at sollicitudin odio. Cras fermentum sapien ut neque volutpat, et pretium lorem cursus. Integer sodales nunc ut tellus accumsan interdum. Pellentesque faucibus, elit ac dignissim egestas, dui urna consectetur lacus, nec pellentesque massa metus eu eros.",
                Author = "Joe Bloggs",
                Posted = DateTime.Now.AddDays(-25)
            },
            new BlogPost {
                Id = 3,
                Title = "WebAssembly FTW",
                Post = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque id fermentum libero. Quisque mattis libero tortor, ac pharetra nunc imperdiet eu. Nulla commodo elit magna, eu suscipit tellus interdum nec. Cras pulvinar aliquam eros vitae posuere. Aliquam sit amet porta lacus. Donec vehicula neque mi, tincidunt convallis neque dignissim congue. Praesent at sollicitudin odio. Cras fermentum sapien ut neque volutpat, et pretium lorem cursus. Integer sodales nunc ut tellus accumsan interdum. Pellentesque faucibus, elit ac dignissim egestas, dui urna consectetur lacus, nec pellentesque massa metus eu eros.",
                Author = "Joe Bloggs",
                Posted = DateTime.Now.AddDays(-20)
            },
            new BlogPost {
                Id = 4,
                Title = "Blazor is Awesome!",
                Post = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque id fermentum libero. Quisque mattis libero tortor, ac pharetra nunc imperdiet eu. Nulla commodo elit magna, eu suscipit tellus interdum nec. Cras pulvinar aliquam eros vitae posuere. Aliquam sit amet porta lacus. Donec vehicula neque mi, tincidunt convallis neque dignissim congue. Praesent at sollicitudin odio. Cras fermentum sapien ut neque volutpat, et pretium lorem cursus. Integer sodales nunc ut tellus accumsan interdum. Pellentesque faucibus, elit ac dignissim egestas, dui urna consectetur lacus, nec pellentesque massa metus eu eros.",
                Author = "Joe Bloggs",
                Posted = DateTime.Now.AddDays(-15)
            },
        };

        [HttpGet(Urls.BlogPosts)]
        public IActionResult BlogPosts()
        {
            return Ok(_blogPosts);
        }

        [HttpGet(Urls.BlogPost)] 
        public IActionResult GetBlogPostById(int id)
        {
            var blogPost = _blogPosts.SingleOrDefault(x => x.Id == id);

            if (blogPost == null)
                return NotFound();

            return Ok(blogPost);
        }
    }
}
