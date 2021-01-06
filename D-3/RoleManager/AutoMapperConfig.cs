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
                p.CreateMap<CourseArrangementEntity, CourseArrangement>()
                                            .ForMember(t => t.EarliestMergeDate, opt => opt.MapFrom(src => src.OperateDate));
                p.CreateMap<CourseArrangementEntity, CourseArrangementSerial>()
                                            .ForMember(t => t.EarliestMergeDate, opt => opt.MapFrom(src => src.OperateDate));
                p.CreateMap<ClassroomEntity, Classroom>();

            }).CreateMapper();
        }
    }

}
