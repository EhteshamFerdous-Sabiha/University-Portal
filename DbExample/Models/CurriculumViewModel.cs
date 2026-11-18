using DbExample.Data.Entities;

namespace DbExample.Models
{
    public class CurriculumViewModel
    {
        public List<Programs> AllPrograms { get; set; } = new();
        public Programs? SelectedProgram { get; set; }
        public Dictionary<int, List<CourseCatalogItemViewModel>> SemesterCurriculum { get; set; } = new();
    }

    public class CourseCatalogItemViewModel
    {
        public int CourseCatalogId { get; set; }
        public int SemesterNo { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Prereq { get; set; } = "Nil";
        public decimal CreditHours { get; set; }
    }
}
