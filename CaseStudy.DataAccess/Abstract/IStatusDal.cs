using CaseStudy.Core.DataAccess;
using CaseStudy.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.DataAccess.Abstract
{
    public interface IStatusDal: IRepository<Status>
    {
    }
}
