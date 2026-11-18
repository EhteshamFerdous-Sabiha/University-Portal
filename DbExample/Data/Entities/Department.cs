using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbExample.Data.Entities;

public partial class Department
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ShortCode { get; set; } = null!;

    public int FacultyId { get; set; }

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual ICollection<FacultyMember> FacultyMembers { get; set; } = new List<FacultyMember>();
}
