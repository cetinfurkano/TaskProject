using CaseStudy.Core.DataAccess.EntityFramework;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.DataAccess.Concrete
{
    public class EfStatusDal: EfRepositoryBase<Status>, IStatusDal
    {
        public EfStatusDal(AppDbContext context) : base(context)
        {

        }
    }
}
