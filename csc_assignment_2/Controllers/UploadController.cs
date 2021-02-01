using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace csc_assignment_2.Controllers
{
    public class UploadController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        // GET: /HelloWorld/Welcome/ 

        public IActionResult Welcome()
        {
            return View();
        }
    }
}