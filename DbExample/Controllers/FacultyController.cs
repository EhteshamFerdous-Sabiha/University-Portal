using DbExample.Data;
using DbExample.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbExample.Controllers
{

   // [Authorize(Roles = "manager,admin")]

    
    public class  FacultyController : Controller
    {

        private AtpdbContext _context;
        public FacultyController(AtpdbContext context)
        {

            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {

            var allfaculties = _context.Faculty.ToList();//data fetch from database and store in variable

            return View(allfaculties);//data pass from controller to view
        }

       // [Route("Faculty/Detail/{facultyId:int}")]//for custom route to avoid query string
        public IActionResult Detail(int facultyId)
        {
            //  return Content(deptId+" ");

            var facultydata = _context.Faculty.Find(facultyId) ?? new Faculty();
            return View(facultydata);
        }

        [HttpPost]
        public IActionResult Detail(Faculty faculty)
        {

            if (ModelState.IsValid==false)
            {
                return View(faculty);
            }


            if(faculty.Id == 0)
            {
                _context.Faculty.Add(faculty);//instead of query to insert data we can use this method
            }
            else
            {
                _context.Faculty.Update(faculty);
            }

            _context.SaveChanges();//to save changes in database

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int facId)
        {
            var data=_context.Faculty.Find(facId);

            if(data == null)
            {
              TempData["Error"] = "Faculty not found";
            }
            else { 
                _context.Faculty.Remove(data);//for list data delete
                _context.SaveChanges();//for database changes save
                TempData["Success"] = "Faculty Deleted Successfully";  
            }
            return RedirectToAction("Index");
        }





        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> FstDetails(string? search)
        {
            // Existing FST Faculty ID
            const int fstFacultyId = 0;

            // Check whether FST exists
            var fst = await _context.Faculty
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == fstFacultyId);

            if (fst == null)
            {
                ViewBag.Search = search;
                ViewBag.Message = "FST faculty was not found.";

                return View(
                    "~/Views/Faculty/FacultyDetails/FstDetails.cshtml",
                    new List<FacultyMember>()
                );
            }

            // Retrieve faculty members belonging to FST departments
            var members = _context.FacultyMember
                .Include(m => m.Department)
                .AsNoTracking()
                .Where(m => m.Department.FacultyId == fstFacultyId);

            // Search by name or research interest
            if (!string.IsNullOrWhiteSpace(search))
            {
                string keyword = search.Trim();

                members = members.Where(m =>
                    m.FullName.Contains(keyword) ||
                    (m.ResearchInterest != null &&
                     m.ResearchInterest.Contains(keyword)));
            }

            ViewBag.Search = search;

            var result = await members
                .OrderBy(m => m.FullName)
                .ToListAsync();

            return View(
                "~/Views/Faculty/FacultyDetails/FstDetails.cshtml",
                result
            );
        }




    }
}
