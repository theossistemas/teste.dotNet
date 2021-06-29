using Livraria.Data;
using Livraria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static Livraria.Models.HomeModel;

namespace Livraria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(new HomeViewModel
            {
                ListAuthors = await _context.Authors.OrderBy(a => a.Name).ToListAsync(),
                ListBooks = await _context.Books
                                 .OrderBy(b => b.Title)
                                 .Include(b => b.Author)
                                 .Include(b => b.Publisher)
                                 .ToListAsync(),
                ListPublishers = await _context.Publishers.OrderBy(a => a.Name).ToListAsync()
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
