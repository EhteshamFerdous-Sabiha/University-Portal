using AutoMapper;
using DbExample.Data.Entities;
using DbExample.DTO;

namespace DbExample.CustomAttribute
{
    public class MappingProfile : Profile

    {
        public MappingProfile()//in same consrtuctor we can write all mapping code
        {
            CreateMap<Department, DepartmentDTO>()
                .ForMember(e => e.FacultyName,
                    s =>
                        s.MapFrom(e=> e.Faculty.Title ));

            CreateMap<DepartmentDTO, Department>();

        }
    }
       
}
