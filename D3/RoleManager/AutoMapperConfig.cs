using AutoMapper;
using D_3.Models.Business;
using D_3.Models.Entities;
using D3.Models.Entities;
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
                p.CreateMap<CourseArrangementEntity, CourseArrangement>()
                                            .ForMember(t => t.EarliestMergeDate, opt => opt.MapFrom(src => src.dtPKDateTime));
                p.CreateMap<CourseArrangementEntity, CourseArrangementSerial>()
                                            .ForMember(t => t.EarliestMergeDate, opt => opt.MapFrom(src => src.dtPKDateTime));
                p.CreateMap<ClassroomEntity, Classroom>();
                p.CreateMap<ClassroomEntity, LogSortedClassroomEntity>();
                p.CreateMap<CourseArrangement, LogSortedCourseArrangementEntity>();
                p.CreateMap<CourseArrangementEntity, LogSortedCourseArrangementEntity>()
                                            .ForMember(t => t.EarliestMergeDate, opt => opt.MapFrom(src => src.dtPKDateTime));
                p.CreateMap<Classroom, LogSortedClassroomEntity>();
                p.CreateMap<CourseArrangementEntity, CourseArrangementQueueEntity>();
                p.CreateMap<CourseArrangement, CourseArrangementEntity>();
            }).CreateMapper();
        }
    }

}
