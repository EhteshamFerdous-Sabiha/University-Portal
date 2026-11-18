using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbExample.Data.Entities;

public partial class Faculty
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual List<Department> Departments { get; set; } = new List<Department>();
}
