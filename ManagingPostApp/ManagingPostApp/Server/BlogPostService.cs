﻿using ManagingPostApp.Shared;

namespace ManagingPostApp.Server
{
    public class BlogPostService
    {
        private List<BlogPost> _blogPosts;

        public BlogPostService()
        {
            _blogPosts = new List<BlogPost>();
        }

        public List<BlogPost> GetBlogPosts()
        {
            return _blogPosts;
        }

        public BlogPost GetBlogPost(int id)
        {
            return _blogPosts.SingleOrDefault(x => x.Id == id);
        }

        public BlogPost AddBlogPost(BlogPost newBlogPost) 
        {
            newBlogPost.Id = _blogPosts.Count + 1;
            _blogPosts.Add(newBlogPost);

            return newBlogPost;
        }
    }
}
