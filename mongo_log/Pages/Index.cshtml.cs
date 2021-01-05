using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MongoDBUtils.Models;

namespace mongo_log.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            var model = new User()
            {
                Name = "小七子",
                Cardcertificate = "123456789",
                Gender = Gender.Boy,
                Houses = new System.Collections.Generic.List<House>() { new House() { Adress = "中关村110号双拼别墅" } }
            };

            var strModel = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(strModel);


        }
    }
}
