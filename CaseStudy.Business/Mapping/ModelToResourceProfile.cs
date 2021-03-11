using AutoMapper;
using CaseStudy.Entity.Concrete;
using CaseStudy.Entity.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Business.Mapping
{
   public class ModelToResourceProfile:Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, SaveUserResource>();
            CreateMap<User, UpdateUserResource > ();

            CreateMap<Status, StatusResource>();
            CreateMap<Status, UpdateStatusResource>();

            CreateMap<UserTask, UserTaskResource>().ForMember(
                src => src.StartingDate,
                opt => opt.MapFrom(src => src.StartingDate.ToString("dd.MM.yyyy"))
                ).ForMember(
                    src => src.DueDate,
                    opt => opt.MapFrom(src => src.DueDate.ToString("dd.MM.yyyy"))
                ).ForMember(
                    src => src.ExpectedDueDate,
                    opt => opt.MapFrom(src => src.ExpectedDueDate.ToString("dd.MM.yyyy"))
                );
        }
    }
}
