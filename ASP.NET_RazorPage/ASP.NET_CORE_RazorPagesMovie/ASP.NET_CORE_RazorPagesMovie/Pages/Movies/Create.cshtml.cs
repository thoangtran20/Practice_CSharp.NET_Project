using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASP.NET_CORE_RazorPagesMovie.Data;
using ASP.NET_CORE_RazorPagesMovie.Models;

namespace ASP.NET_CORE_RazorPagesMovie.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly ASP.NET_CORE_RazorPagesMovie.Data.ApplicationDbContext _context;

        public CreateModel(ASP.NET_CORE_RazorPagesMovie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Movie == null || Movie == null)
            {
                return Page();
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
