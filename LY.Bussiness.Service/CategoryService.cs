using LY.Bussiness.Interface;
using LY.EF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.Bussiness.Service
{
    public class CategoryService: BaseService, ICategoryService
    {
        public CategoryService(DbContext context) : base(context){

        }
    }
}
