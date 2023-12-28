﻿using CVDataLayer;
using CVModels;
using Microsoft.AspNetCore.Mvc;

namespace CV_Hemsida.Controllers
{
    public class HomeController : Controller
    {
        private CVContext _dbContext;

        public HomeController(CVContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
