using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmarkHealthKidoPack.Models;
using SmarkHealthKidoPack.ViewModel;

namespace SmarkHealthKidoPack.Controllers
{
    public class AdminController : Controller
    {
        public readonly MainContext _Context;
        public IHostingEnvironment HostingEnvironment { get; }

        public AdminController(MainContext context, IHostingEnvironment hostingEnvironment)
        {
            _Context = context;
            HostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
       

        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(AdminViewModel admin)


        {


            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (admin.Photo != null)
                {
                    string uploadFolder = Path.Combine(HostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + admin.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    admin.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

                }
                Admin newAdmin = new Admin
                {

                    AdminName = admin.AdminName,
                    Passward = admin.password,
                    photopath = uniqueFileName
                };
                _Context.Add(newAdmin);







                _Context.SaveChanges();
                
                return RedirectToAction(nameof(LogIn));

            }
            return View(admin);
        }

        public IActionResult Welcome(string user, string pass)
        {
            var uni = _Context.admin
                  .FirstOrDefault(m => m.AdminName == user);

            if (uni != null)
            {

                //TempData["Message"] = user;
                HttpContext.Session.SetString("sessionUser", Newtonsoft.Json.JsonConvert.SerializeObject(uni));

               
            }
            else
            {
                return RedirectToAction(nameof(LogIn));
            }
            return View();
        }


     

        public IActionResult CreateFood()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateFood(Foodviewmode food)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (food.photo != null)
                {
                    string uploadFolder = Path.Combine(HostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + food.photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    food.photo.CopyTo(new FileStream(filePath, FileMode.Create));

                }
                Food newfood = new Food
                {

                    FoodName = food.foodName,
                    foodCalories = food.foodCalories,
                    photopath = uniqueFileName
                };

                //if (Image != null)
                _Context.Add(newfood);







                _Context.SaveChanges();
                return RedirectToAction(nameof(FoodList));

            }

            return View();
        }

        public IActionResult FoodList()
        {


            return View(_Context.food.ToList());
        }
        public async Task<IActionResult> EditView(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //photopath
            var food = await _Context.food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            //IFormFile
            //Foodviewmode newfood = new Foodviewmode
            //{
            //    FoodId = food.FoodId,
            //    foodName = food.FoodName,
            //    foodCalories = food.foodCalories,
            //    photo=food.photopath

            //};

            return View(food);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditView(int id, Food food)
        {
            if (id != food.FoodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Context.Update(food);
                    await _Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction(nameof(FoodList));
            }
            return View(food);
        }


        private bool FoodExists(int id)
        {
            return _Context.food.Any(e => e.FoodId == id);
        }

    }
}