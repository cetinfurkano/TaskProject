using Autofac;
using AutoMapper;
using CaseStudy.Business.Abstract;
using CaseStudy.Business.Concrete;
using CaseStudy.Core.DataAccess;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.DataAccess.Concrete;
using CaseStudy.DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Business.IoC.Autofac
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>().InstancePerLifetimeScope();

            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<StatusService>().As<IStatusService>().SingleInstance();
            builder.RegisterType<EfStatusDal>().As<IStatusDal>().SingleInstance();

            builder.RegisterType<UserTaskService>().As<IUserTaskService>().SingleInstance();
            builder.RegisterType<EfUserTaskDal>().As<IUserTaskDal>().SingleInstance();

            builder.RegisterType<EfUnitOfWork>().As<IUnitOfWork>().SingleInstance();

            //builder.Register(context => new MapperConfiguration());           
        }
    }
}
