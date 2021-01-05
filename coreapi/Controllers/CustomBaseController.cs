using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace coreapi.Controllers
{
    //[AllowAnonymous]
    [Authorize]
    public class CustomBaseController : ControllerBase
    {

        //今后 
    } 
}
      