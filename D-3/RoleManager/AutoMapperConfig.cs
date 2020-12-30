using AutoMapper;
using D_3.Models.Business;
using D_3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.RoleManager
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper;
        static AutoMapperConfig()
        {
            Mapper = new MapperConfiguration(p =>
            {
                p.CreateMap<CourseArrangementEntity, CourseArrangement>();
                p.CreateMap<CourseArrangementEntity, CourseArrangementSerial>();
                p.CreateMap<ClassroomEntity, Classroom>();

            }).CreateMapper();
        }
    }

}
