using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NET_CORE_RazorPagesMovie.Data;
using ASP.NET_CORE_RazorPagesMovie.Models;

namespace ASP.NET_CORE_RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly ASP.NET_CORE_RazorPagesMovie.Data.ApplicationDbContext _context;

        public IndexModel(ASP.NET_CORE_RazorPagesMovie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }   
        public SelectList? Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }


        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                          orderby m.Genre
                                          select m.Genre;
            var movies = from m in _context.Movie 
                        select m;
            //if (_context.Movie != null)
            //{
            //    Movie = await _context.Movie.ToListAsync();
            //}
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();  
        }
    }
}
