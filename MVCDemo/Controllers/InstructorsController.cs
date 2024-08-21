using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCDemo.Data;
using MVCDemo.Models;
using System.Linq;
using System.Threading.Tasks;

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

        // GET: Instructor/Create
        [HttpGet]
        public IActionResult Create()
        {
            // Here you can prepare ViewBag for dropdown lists (e.g., DeptId, CourseId)
            ViewBag.DeptId = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.CourseId = new SelectList(_context.Courses, "Id", "Name");

            return View();
        }

        // POST: Instructor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Instructors.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If the model state is not valid, redisplay the form with validation errors
            ViewBag.DeptId = new SelectList(_context.Departments, "Id", "Name", instructor.DeptId);
            ViewBag.CourseId = new SelectList(_context.Courses, "Id", "Name", instructor.CourseId);

            return View(instructor);
        }


        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            // Populate dropdown lists for editing
            ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Name", instructor.DeptId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", instructor.CourseId);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image,Salary,Address,DeptId,CourseId")] Instructor instructor)
        {
            if (id != instructor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdown lists if the model state is invalid
            ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Name", instructor.DeptId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", instructor.CourseId);
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.Id == id);
        }
    }
}
