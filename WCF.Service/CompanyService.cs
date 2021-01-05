using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.IService;
using WCF.Model;

namespace WCF.Service
{
    public class CompanyService : ICompanyService
    {
        public Company Add(Company model)
        {
          
            return model;
        }

        public bool Delete(long Id)
        {
            return Id > 0;
        }

        public IList<Company> GetAllList()
        {
            return new List<Company> { new Company {  CreateTime=DateTime.Now, Name="xkm", Id=1, PassWord="123456" } };
        }

        public IList<Company> GetList(string Name)
        {
            return new List<Company> { };
        }
    }
}
