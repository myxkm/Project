using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.Framework.WorkContext
{
    public class CurrentUser
    {
        public int Id { get; set; }
        //public IList<MenuInfo> Menus { get; set; }
        public IList<string> Actions { get; set; }
        public DateTime LoginTime { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string StaffsPhoto { get; set; }
    }
}
