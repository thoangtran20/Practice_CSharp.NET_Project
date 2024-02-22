using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.NET_CORE_RazorPagesMovie.Data;
using ASP.NET_CORE_RazorPagesMovie.Models;

namespace ASP.NET_CORE_RazorPagesMovie.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly ASP.NET_CORE_RazorPagesMovie.Data.ApplicationDbContext _context;

        public DetailsModel(ASP.NET_CORE_RazorPagesMovie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Movie Movie { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }
            else 
            {
                Movie = movie;
            }
            return Page();
        }
    }
}
