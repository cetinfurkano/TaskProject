using AutoMapper;
using CaseStudy.Entity.Concrete;
using CaseStudy.Entity.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Business.Mapping
{
    public class ResourceToModelProfile:Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();
            CreateMap<UpdateUserResource, User>();

            CreateMap<StatusResource, Status>();
            CreateMap<UpdateStatusResource, Status>();

            CreateMap<UserTaskResource, UserTask>();
            
        }
    }
}
