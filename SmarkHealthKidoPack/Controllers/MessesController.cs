using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmarkHealthKidoPack.Models;
using SmarkHealthKidoPack.ViewModel;

namespace SmarkHealthKidoPack.Controllers
{
    public class MessesController : Controller
    {
        public readonly MainContext _Context;
        public IHostingEnvironment HostingEnvironment { get; }
       // private readonly UserManager<ApplicationUser> userManager;
        public MessesController(MainContext context, IHostingEnvironment hostingEnvironment)
        {
            _Context = context;
            HostingEnvironment = hostingEnvironment;

        }
        public async Task<IActionResult> Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(MessViewModel mess)
        {

            if (ModelState.IsValid)
            {

                string uniqueFileName = Photopathadmin(mess);



                Mess m = new Mess
                {
                    MessName = mess.messName,
                    Password = mess.password,
                    photopath = uniqueFileName
                };
                _Context.Add(m);

                _Context.SaveChanges();

                return RedirectToAction(nameof(LogIn));
            }
            return View(mess);
        }

        public async Task<IActionResult> LogIn()
        {
            if (HttpContext.Session.GetString("sessionUser1234") != null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["Test"] = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(Mess m1)
        {

            var mess = _Context.Messes
                 .FirstOrDefault(m => m.MessName == m1.MessName && m.Password == m1.Password);
            // var mess = _context.Mess
            //.Where(l => l.MessName == m.MessName && l.Password == m.Password);

            if (mess != null)/* && passward !=null*/
            {
                //create session
                HttpContext.Session.SetString("sessionUser1234", Newtonsoft.Json.JsonConvert.SerializeObject(mess));
                //  HttpContext.Session.SetString("sessionUser", Newtonsoft.Json.JsonConvert.SerializeObject(mess));
                return RedirectToAction(nameof(Index));

            }
            ViewData["Test"] = true;
            return View();
        }
        /// <summary>
        /// change
        /// </summary>
        /// <returns></returns>
        // GET: Messes
        public async Task<IActionResult> Index()
        {

            var Messinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));


            int id = Messinfo.MessId;
            var add = _Context.food.Include(c => c.foodCategory).Where(c=>c.foodCategory.MessId==id).ToList();
            var welcomeVM = new WelcomeViewModel
            
            {
                CustomersCount = 0,
                FoodCount = _Context.food.Include(c => c.foodCategory).Where(c => c.foodCategory.MessId == id).Count(x =>true),
            adminCount = 0,
                categoryCount = _Context.foodCategories.Where(m =>m.MessId==id).Count(x => true),
                food = add
            };
            return View(welcomeVM);
        }


        public string Photopathadmin(MessViewModel mess)
        {

            string uniqueFileName = null;
            if (mess.Photo != null)
            {
                string uploadFolder = Path.Combine(HostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + mess.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                mess.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

            }
            return uniqueFileName;
        }
        public ActionResult LogOut()
        {
            //clear session
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(LogIn));
            //return View("LogIn");
        }
        public IActionResult myprofile()
        {
            var userinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));
            int id = userinfo.MessId;
            var adminpro = _Context.Messes.FirstOrDefault(m => m.MessId == id);
            Mess ad = new Mess
            {
                MessId = adminpro.MessId,
                MessName = adminpro.MessName,
                photopath = adminpro.photopath


            };


            return View(ad);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

 
        public IActionResult ListOfUsers()
        {
            var userinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));
            int id = userinfo.MessId;
        var list=    _Context.guardians.Include(m => m.child).Where(m => m.messId == id).ToList();
            return View(list);
        }

    }
}