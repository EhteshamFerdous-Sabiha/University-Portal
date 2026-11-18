using DbExample.Data;
using DbExample.Data.Entities;
using DbExample.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DbExample.Controllers
{
    public class FacultyMemberController : Controller
    {
        private readonly AtpdbContext _context;

        public FacultyMemberController(AtpdbContext context)
        {
            _context = context;
        }

        // Public search
        [AllowAnonymous]
        public async Task<IActionResult> Index(string? search)
        {
            var members = _context.FacultyMember
                .Include(f => f.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                members = members.Where(f =>
                    f.FullName.Contains(search) ||
                    (f.ResearchInterest != null &&
                     f.ResearchInterest.Contains(search)));
            }

            ViewBag.Search = search;

            return View(await members.ToListAsync());
        }

        // Admin - Create 
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(
                _context.Department.OrderBy(d => d.Name).ToList(),
                "Id",
                "Name"
            );

            return View(new FacultyMemberFormDTO
            {
                JoinDate = DateTime.Today
            });
        }

        // Admin - Create POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacultyMemberFormDTO model)
        {
            // Check whether the selected department exists
            if (model.DepartmentId.HasValue)
            {
                bool departmentExists = await _context.Department
                    .AnyAsync(d => d.Id == model.DepartmentId.Value);

                if (!departmentExists)
                {
                    ModelState.AddModelError(
                        nameof(model.DepartmentId),
                        "Selected department does not exist.");
                }
            }

            // Check duplicate email
            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                bool emailExists = await _context.FacultyMember
                    .AnyAsync(m => m.Email == model.Email.Trim());

                if (emailExists)
                {
                    ModelState.AddModelError(
                        nameof(model.Email),
                        "This email is already registered.");
                }
            }

            // If validation fails, show the form again
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(
                    _context.Department.OrderBy(d => d.Name).ToList(),
                    "Id",
                    "Name",
                    model.DepartmentId
                );

                return View(model);
            }

            // Convert DTO into FacultyMember entity
            var member = new FacultyMember
            {
                FullName = model.FullName.Trim(),
                Email = model.Email.Trim(),
                Phone = model.Phone?.Trim(),
                ResearchInterest = model.ResearchInterest?.Trim(),
                Designation = model.Designation.Trim(),
                JoinDate = model.JoinDate!.Value,
                DepartmentId = model.DepartmentId!.Value
            };

            // Insert into database
            _context.FacultyMember.Add(member);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Faculty member added successfully.";

            return RedirectToAction("FstDetails", "Faculty");
        }

        // Admin - Edit GET
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var member = await _context.FacultyMember
                .FindAsync(id);

            if (member == null)
                return NotFound();

            ViewBag.Departments = new SelectList(
                _context.Department,
                "Id",
                "Name",
                member.DepartmentId
            );

            return View(member);
        }

        // Admin - Edit POST
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("FullName,Email,Phone,ResearchInterest,Designation,JoinDate,DepartmentId")]
            FacultyMember form)
        {
            var member = await _context.FacultyMember
                .FindAsync(id);

            if (member == null)
                return NotFound();

            ModelState.Remove(nameof(FacultyMember.Department));

            if (ModelState.IsValid)
            {
                member.FullName = form.FullName;
                member.Email = form.Email;
                member.Phone = form.Phone;
                member.ResearchInterest = form.ResearchInterest;
                member.Designation = form.Designation;
                member.JoinDate = form.JoinDate;
                member.DepartmentId = form.DepartmentId;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(
                _context.Department,
                "Id",
                "Name",
                form.DepartmentId
            );

            return View(form);
        }

        // Admin - Delete POST
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _context.FacultyMember
                .FindAsync(id);

            if (member == null)
                return NotFound();

            _context.FacultyMember.Remove(member);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}