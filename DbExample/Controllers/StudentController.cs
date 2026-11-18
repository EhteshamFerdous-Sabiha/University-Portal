using DbExample.Data;
using DbExample.Data.Entities;
using DbExample.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DbExample.Controllers
{
    public class StudentController : Controller
    {
        private readonly AtpdbContext _context;

        public StudentController(AtpdbContext context)
        {
            _context = context;
        }

        // Student List
        [HttpGet]
        public async Task<IActionResult> StudentIndex()
        {
            var students = await _context.Students
                .Include(s => s.Program)
                .AsNoTracking()
                .OrderBy(s => s.StudentName)
                .ToListAsync();

            ViewBag.TotalStudents = students.Count;
            ViewBag.ActiveStudents = students.Count(s => s.Status == "Active");
            ViewBag.InactiveStudents = students.Count(s => s.Status == "Inactive");

            return View(students);
        }

        // Student Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var student = await _context.Students
                .Include(s => s.Program)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // GET: Student/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadProgramsDropdownAsync();

            var model = new StudentDTO
            {
                Status = "Active",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20)),
                Validity = DateOnly.FromDateTime(DateTime.Today.AddYears(4))
            };

            return View(model);
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDTO model)
        {
            // Check for duplicate Student ID/No
            if (!string.IsNullOrWhiteSpace(model.StudentNo))
            {
                bool idExists = await _context.Students
                    .AnyAsync(s => s.StudentNo == model.StudentNo.Trim());

                if (idExists)
                {
                    ModelState.AddModelError(nameof(model.StudentNo), "A student with this Student ID already exists.");
                }
            }

            // Check for duplicate Email
            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                bool emailExists = await _context.Students
                    .AnyAsync(s => s.Email == model.Email.Trim());

                if (emailExists)
                {
                    ModelState.AddModelError(nameof(model.Email), "This varsity email is already registered.");
                }
            }

            if (!ModelState.IsValid)
            {
                await LoadProgramsDropdownAsync(model.ProgramId);
                return View(model);
            }

            // Map DTO to Student Entity
            var student = new Student
            {
                StudentNo = model.StudentNo.Trim(),
                StudentName = model.StudentName.Trim(),
                Email = model.Email.Trim(),
                Phone = model.Phone.Trim(),
                DateOfBirth = model.DateOfBirth,
                Validity = model.Validity,
                ProgramId = model.ProgramId,
                Status = string.IsNullOrWhiteSpace(model.Status) ? "Active" : model.Status
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Student '{student.StudentName}' was registered successfully!";
            return RedirectToAction(nameof(StudentIndex));
        }

        private async Task LoadProgramsDropdownAsync(int? selectedProgramId = null)
        {
            var programs = await _context.Programs
                .AsNoTracking()
                .OrderBy(p => p.ProgramName)
                .ToListAsync();

            ViewBag.Programs = new SelectList(programs, "ProgramId", "ProgramName", selectedProgramId);
        }
    }
}
