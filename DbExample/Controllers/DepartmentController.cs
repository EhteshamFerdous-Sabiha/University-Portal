using AutoMapper;
using DbExample.Data;
using DbExample.Data.Entities;
using DbExample.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;


namespace DbExample.Controllers
{
    public class DepartmentController : Controller
    {

        private AtpdbContext _context;
        private IMapper _mapper;
        public DepartmentController(AtpdbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {

            var allDepartments = _context.Department.
                Include(d => d.Faculty).ToList();

            var dtos= _mapper.Map<List<DepartmentDTO>>(allDepartments);//for database data pass  DTO version to view using automapper

            return View(dtos);//data pass from controller to view
        }

       // [Route("Department/Detail/{deptId:int}")]//for custom route to avoid query string
        public IActionResult Detail(int deptId)
        {
            var dept=_context.Department.Find(deptId)
                ??new Department();


            var faculties = _context.Faculty.ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var faculty in faculties)
            {
                var item = new SelectListItem
                {
                    Text = faculty.Title,
                    Value = faculty.Id.ToString()
                };
                items.Add(item);
            }
            ViewBag.Faculties = items;



            //DTO 


            /*            var DTO = new DepartmentDTO   
                        var DTO = new DepartmentDTO
                        {
                            Id = dept.Id,
                            Name = dept.Name,
                            ShortCode = dept.ShortCode,
                            FacultyId = dept.FacultyId,

                        }; */

            //return View(dept);
           // return View(DTO);//database data pass  DTO version to view

             var dto = _mapper.Map<DepartmentDTO>(dept);//for database data pass  DTO version to view using automapper
             return View(dto);
        }
        [HttpPost]
        public IActionResult Detail(DepartmentDTO dto)
        {
            if (!_context.Faculty.Any(f => f.Id == dto.FacultyId))
            {
                ModelState.AddModelError(nameof(dto.FacultyId), "Select an existing faculty.");
            }

            if (ModelState.IsValid == false)
            {
                // Reload the faculty dropdown list
                var faculties = _context.Faculty.ToList();

                List<SelectListItem> items = new List<SelectListItem>();

                foreach (var faculty in faculties)
                {
                    var item = new SelectListItem
                    {
                        Text = faculty.Title,
                        Value = faculty.Id.ToString()
                    };

                    items.Add(item);
                }

                ViewBag.Faculties = items;

                return View(dto);
            }

            var dept = _mapper.Map<Department>(dto);

            if (dept.Id == 0)
            {
                _context.Department.Add(dept);
            }
            else
            {
                _context.Department.Update(dept);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int deptId)
        {
            var dept=_context.Department.Find(deptId);

            if(dept == null)
            {

            }
            else { 
                _context.Department.Remove(dept);//for list data delete
                _context.SaveChanges();//for database changes save
            }
            return RedirectToAction("Index");
        }




        private void LoadFaculties(int selectedFacultyId = 0)
        {
            ViewBag.Faculties = new SelectList(
                _context.Faculty
                    .OrderBy(f => f.Title)
                    .ToList(),
                "Id",
                "Title",
                selectedFacultyId
            );
        }

    }
}