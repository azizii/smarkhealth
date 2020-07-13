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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mess>>> GetMesses()
        {

            //All food table record
            var ad1 = _Context.Messes.ToList();



            return ad1;
        }
        /// <summary>
        /// get request for food
        /// </summary>
        /// <param name="childfood"></param>
        /// <returns></returns>sss
        [HttpPost]
        public string PostMessage([FromBody]string messagebody1)
        {
            if (messagebody1 != null)
            {
                Guardian userinfo = JsonConvert.DeserializeObject<Guardian>(HttpContext.Session.GetString("Guardian"));


                Messages m = new Messages
                {
                    Messagebody = messagebody1,
                    GuardianId = userinfo.GuardianId,
                    messagedate = DateTime.Now,
                    messids= userinfo.messId
                };
                _Context.Add(m);
                _Context.SaveChanges();
                return JsonConvert.SerializeObject("message send succesfully");
            }
            else
            {
                return JsonConvert.SerializeObject("message send succesfully");
            }
           
          

            


        }

        /// <summary>
        /// messages seen my admin
        /// </summary>
        /// <returns></returns>
        public  IActionResult SeeMessages()
        {
            var Messinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));


            int id = Messinfo.MessId;
          var messsage=   _Context.Messages.Include(m=>m.Guardian).Where(c => c.messids == id).ToList();
            ViewBag.count = messsage.Count;
            if (messsage.ToList().Count == 0)
            {
                return View("Empty");
            }
            return View(messsage);
        }

        public IActionResult seeguardianmessage(int id)
        {
            var Messinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));
            var messsage = _Context.Messages.Include(m => m.Guardian).Where(c => c.GuardianId==id).ToList();

            int messid = Messinfo.MessId;
            ViewBag.count = messsage.Count;
            if (messsage.ToList().Count == 0)
            {
                return View("Empty");
            }
            return View(messsage);
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
                CustomersCount = _Context.guardians.Where(m => m.messId == id).Count(x => true),
                FoodCount = _Context.food.Include(c => c.foodCategory).Where(c => c.foodCategory.MessId == id).Count(x =>true),
            adminCount = 1,
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


        public async Task<IActionResult> addcradits(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            //photopath
            var guardain = await _Context.guardians.FindAsync(id);

          //  TempData["pic"] = food.photopath;
            if (guardain == null)
            {
                return NotFound();
            }
            //IFormFile

            GuardianBalanaceViewModel g = new GuardianBalanaceViewModel
            {
                GuardianId = guardain.GuardianId,
                Guardianname = guardain.GuardianName,
                phonenumber = guardain.phonenumber,
                adress = guardain.adress,
                 OldBalance = guardain.Balance,
                messId= guardain.messId,
                passward= guardain.passward,
                newBalance=0


            };
            //var userinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));
            //int idf = userinfo.MessId;
            //ViewBag.foodCategoryId = new SelectList(_Context.foodCategories.Where(m => m.MessId == idf), "FoodCategoryId", "FoodCategoryName");
            return View(g);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addcradits(int id, GuardianBalanaceViewModel guardian)
        {
          //  var Messinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));


           // int messid = Messinfo.MessId;
            if (id != guardian.GuardianId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
              
                   int balance = 0;


                 
                   balance = guardian.OldBalance + guardian.newBalance;
                  


                    Guardian g = new Guardian
                    {
                        GuardianId = guardian.GuardianId,
                        GuardianName = guardian.Guardianname,
                        phonenumber = guardian.phonenumber,
                        adress = guardian.adress,
                        Balance = balance,
                        messId = guardian.messId,
                        passward= guardian.passward


                    };







                    _Context.Update(g);
                    await _Context.SaveChangesAsync();
                
         
                return RedirectToAction(nameof(ListOfUsers));
            }
            return View(guardian);
        }
    }
}