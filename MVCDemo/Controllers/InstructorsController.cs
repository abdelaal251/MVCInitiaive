using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCDemo.Data;
using MVCDemo.Models;
using System.Threading.Tasks;
using System.Linq;

namespace MVCDemo.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Instructors
        public async Task<IActionResult> Index()
        {
            // Load instructors with related Course and Department data
            var instructors = await _context.Instructors
                .Include(i => i.Course)
                .Include(i => i.Department)
                .ToListAsync();
            return View(instructors);
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Load the instructor with related Course and Department data
            var instructor = await _context.Instructors
                .Include(i => i.Course)
                .Include(i => i.Department)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }
    }
}
