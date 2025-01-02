﻿using SmprMvcApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmprMvcApp.DAL.Repository.Interface
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company entity);
    }
}