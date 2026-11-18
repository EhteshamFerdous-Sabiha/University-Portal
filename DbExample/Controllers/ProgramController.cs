using DbExample.Data;
using DbExample.Data.Entities;
using DbExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbExample.Controllers
{
    public class ProgramController : Controller
    {
        private readonly AtpdbContext _context;

        public ProgramController(AtpdbContext context)
        {
            _context = context;
        }

        // GET: /Program/Curriculum?programId=1
        [HttpGet]
        public async Task<IActionResult> Curriculum(int? programId)
        {
            var allPrograms = await _context.Programs
                .Include(p => p.Department)
                .AsNoTracking()
                .OrderBy(p => p.ProgramId)
                .ToListAsync();

            if (!allPrograms.Any())
            {
                return View(new CurriculumViewModel());
            }

            var selectedProg = allPrograms.FirstOrDefault(p => p.ProgramId == programId)
                               ?? allPrograms.FirstOrDefault(p => p.DepartmentId == 4)
                               ?? allPrograms.First();

            var catalogs = await _context.CourseCatalogs
                .Include(cc => cc.Course)
                .Where(cc => cc.ProgramId == selectedProg.ProgramId)
                .AsNoTracking()
                .OrderBy(cc => cc.SemesterNo)
                .ThenBy(cc => cc.Course != null ? cc.Course.Code : "")
                .ToListAsync();

            var grouped = catalogs
                .GroupBy(c => c.SemesterNo)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(c => new CourseCatalogItemViewModel
                    {
                        CourseCatalogId = c.CourseCatalogId,
                        SemesterNo = c.SemesterNo,
                        Code = c.Course?.Code ?? "N/A",
                        Title = c.Course?.Title ?? "N/A",
                        Prereq = string.IsNullOrWhiteSpace(c.Course?.Prereq) ? "Nil" : c.Course.Prereq,
                        CreditHours = c.Course?.CreditHours ?? 0
                    }).ToList()
                );

            var viewModel = new CurriculumViewModel
            {
                AllPrograms = allPrograms,
                SelectedProgram = selectedProg,
                SemesterCurriculum = grouped
            };

            return View(viewModel);
        }
    }
}
