using LY.Bussiness.Interface;
using System.Data.Entity;

namespace LY.Bussiness.Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(DbContext context) : base(context)
        {
        }
    }
}
