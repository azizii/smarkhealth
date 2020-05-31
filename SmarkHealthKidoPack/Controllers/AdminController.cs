using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmarkHealthKidoPack.Models;
using SmarkHealthKidoPack.ViewModel;

namespace SmarkHealthKidoPack.Controllers
{
    public class AdminController : Controller
    {
        private const string ChampionsImageFolder = "images";
        public readonly MainContext _Context;
        public IHostingEnvironment HostingEnvironment { get; }

        public AdminController(MainContext context, IHostingEnvironment hostingEnvironment)
        {
            _Context = context;
            HostingEnvironment = hostingEnvironment;
        }


        //public JsonResult getadmin()
        //{



        //    //var path = Path.Combine(HostingEnvironment.WebRootPath, ChampionsImageFolder);

        //    //var json = JsonConvert.SerializeObject(path);

        //    //return Json(json);
        //    List<Admin> ad = _Context.admin.ToList();

        //    string serverPathToChampionsFolder = "wwwroot/images/";

        //    List<string> intList = new List<string>();

        //    foreach (var champion in ad)
        //    {


        //        intList.Add(serverPathToChampionsFolder + champion.photopath);
        //    }
        //    //var filePath = Path.Combine();
        //    ////var filePath = Path.Combine(HostingEnvironment.WebRootPath, "images");

        //    var json = JsonConvert.SerializeObject(intList);

        //    return Json(json);
        //}







        //        public IActionResult Register()
        //        {
        //            add();
        //            return View();
        //        }
        //        [HttpPost]
        //        public IActionResult Register(AdminViewModel admin)


        //        {
        //            //if(admin.AdminName!=null || admin.password !=null)
        //            //{



        //                if (ModelState.IsValid)
        //                {

        //                    string uniqueFileName = Photopathadmin(admin);


        //                    Admin newAdmin = new Admin
        //                    {

        //                        AdminName = admin.AdminName,
        //                        Passward = admin.password,
        //                        photopath = uniqueFileName
        //                    };
        //                    _Context.Add(newAdmin);

        //                    _Context.SaveChanges();

        //                    return RedirectToAction(nameof(LogIn));
        //                }
        //            //}
        //            return View(admin);
        //        }

        //        public void add()
        //        {
        //            Admin newAdmin = new Admin
        //            {

        //                AdminName = "",
        //                Passward = "",
        //                photopath = ""
        //            };
        //            _Context.Add(newAdmin);

        //            _Context.SaveChanges();
        //        }
        //        public IActionResult LogIn()
        //        {

        //            if (HttpContext.Session.GetString("sessionUser") != null)
        //            {
        //                return RedirectToAction(nameof(Welcome));
        //            }
        //            ViewData["Test"] = false;
        //            return View();
        //        }

        //        [HttpPost]
        //        public IActionResult LogIn(Admin admin)
        //        {
        //            var adminname = _Context.admin
        //                  .FirstOrDefault(m => m.AdminName == admin.AdminName && m.Passward ==admin.Passward);
        //            //var passward = _Context.admin
        //            //      .FirstOrDefault(m => m.Passward == admin.Passward);
        //            if (adminname != null )/* && passward !=null*/
        //                {
        // //create session
        //                HttpContext.Session.SetString("sessionUser", Newtonsoft.Json.JsonConvert.SerializeObject(adminname));
        //                return RedirectToAction(nameof(Welcome));
        //               }
        //            ViewData["Test"] = true;

        //            return View(admin);
        //        }

        //        public IActionResult Welcome()
        //        {

        //            if (HttpContext.Session.GetString("sessionUser") == null)
        //            {
        //                return RedirectToAction(nameof(LogIn));



        //            }

        //            var welcomeVM = new WelcomeViewModel
        //            {
        //                CustomersCount = 0,
        //                FoodCount = _Context.food.Count(x => true),
        //                adminCount = _Context.admin.Count(x => true),
        //                categoryCount = _Context.foodCategories.Count(x => true),
        //                food=_Context.food.Include(m => m.foodCategory).ToList()
        //            };
        //            IList<Food> studentList = _Context.food.ToList();




        //            return View(welcomeVM);
        //        }






        //        public string Photopathadmin(AdminViewModel admin)
        //        {

        //            string uniqueFileName = null;
        //            if (admin.Photo != null)
        //            {
        //                string uploadFolder = Path.Combine(HostingEnvironment.WebRootPath, "images");
        //                uniqueFileName = Guid.NewGuid().ToString() + "_" + admin.Photo.FileName;
        //                string filePath = Path.Combine(uploadFolder, uniqueFileName);
        //                admin.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

        //            }
        //            return uniqueFileName;
        //        }


        //        public ActionResult LogOut()
        //        {
        //            //clear session
        //            HttpContext.Session.Clear();
        //            return RedirectToAction(nameof(LogIn));
        //            //return View("LogIn");
        //        }


        //        public IActionResult myprofile()
        //        {
        //            var userinfo = JsonConvert.DeserializeObject<Admin>(HttpContext.Session.GetString("sessionUser"));
        //            int id = userinfo.AdminId;
        //            var adminpro = _Context.admin.FirstOrDefault(m => m.AdminId == id);
        //            Admin ad = new Admin
        //            {
        //                AdminId=adminpro.AdminId,
        //                AdminName = adminpro.AdminName,
        //             photopath= adminpro.photopath


        //            };


        //            return View(ad);
        //        }
        //        public IActionResult SeeAdmins()
        //        {

        //            return View(_Context.admin.ToList());
        //        }
        //        public IActionResult Dashboard()
        //        {
        //            return View();
        //        }
        //        public IActionResult ChildList()
        //        {
        //            List<Child> products = new List<Child>() {
        //                new Child()
        //                {
        //                   ChildId = 1,
        //                   ChildName = "Zain",


        //                },
        //                new Child()
        //                {
        //                     ChildId = 2,
        //                   ChildName = "arshad",
        //                },
        //                new Child()
        //                {
        //                 ChildId = 3,
        //                   ChildName = "Wajahat",
        //                }
        //            };
        //            ViewBag.products = products;

        //            return View();
        //        }

    
    }
}