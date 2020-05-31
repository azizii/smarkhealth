using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmarkHealthKidoPack.Models;

namespace SmarkHealthKidoPack.Controllers
{
    public class ChildController : Controller
    {
        public readonly MainContext _Context;
        public ChildController(MainContext context)
        {
            _Context = context;
    

        }

        public IActionResult LogIn()
        {
            ViewData["Test"] = false;
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(Child child)
        {
            if (child==null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var c = _Context.children.Include(m =>m.Guardian).FirstOrDefault(m => m.ChildName == child.ChildName && m.password == child.password);

                TempData["id"] = c.ChildId;
                // 
                if (c!=null)
                {

                    var Messinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));


                    int id = Messinfo.MessId;
                    if (c.Guardian.messId == id) {
                        return RedirectToAction(nameof(Index));
                    }
                    else{
                        ViewBag.message = "user is not register yet";
                        //ViewData["Test"] = true;
                    }
                }
                ViewData["Test"] = true;
            }
            return View();
        }


        public IActionResult Index()
        {
            int childid = Convert.ToInt32(TempData["id"].ToString());
            var contextClass = _Context.childFoods.Include(a => a.child).Include(a => a.Food).Where(c=>c.ChildId== childid);
            return View(contextClass.ToList());
        }
    }
}