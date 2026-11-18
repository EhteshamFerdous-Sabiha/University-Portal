using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbExample.Data.Entities;

public partial class FacultyMember
{
    [Key]

    public int FacultyMemberId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? ResearchInterest { get; set; }

    public string? Designation { get; set; }

    public DateTime JoinDate { get; set; }

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;
}
