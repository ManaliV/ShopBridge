using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShopBridge.Web.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}