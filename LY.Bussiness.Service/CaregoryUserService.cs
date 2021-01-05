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
    public class CaregoryUserService : BaseService, ICaregoryUserService
    {
        public CaregoryUserService(DbContext context) : base(context)
        {
            
        }

        //public CaregoryUser AddCaregoryUser() {

        //}
    }
}
