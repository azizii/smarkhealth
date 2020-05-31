using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmarkHealthKidoPack.Models;
using SmarkHealthKidoPack.ViewModel;

namespace SmarkHealthKidoPack.Controllers
{
   
  
    public class FoodController : Controller
    {



        public readonly MainContext _Context;
        public IHostingEnvironment HostingEnvironment { get; }

        public FoodController(MainContext context, IHostingEnvironment hostingEnvironment)
        {
            _Context = context;
            HostingEnvironment = hostingEnvironment;
        }

        public IActionResult CreateFood()
        {
            int id = 0;
            try
            {
                var userinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));
                id = userinfo.MessId;
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogIn", "Messes");
            }
            //var userinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));

            // _Context.Set<FoodCategory>()
            ViewBag.foodCategoryId = new SelectList(_Context.foodCategories.Where(m => m.MessId == id), "FoodCategoryId", "FoodCategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult CreateFood(Foodviewmode food)
        {
            var userinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));
            int id = userinfo.MessId;
            if (ModelState.IsValid)
            {

                string uniqueFileName = Photopathfood(food);

                Food newfood = new Food
                {

                    FoodName = food.foodName,
                    foodCalories = food.foodCalories,
                    photopath = uniqueFileName,
                    foodCategoryId = food.foodCategoryId,
                    Price = food.price


                };

                //if (Image != null)
                _Context.Add(newfood);







                _Context.SaveChanges();
                return RedirectToAction(nameof(FoodList));

            }
            ViewBag.foodCategoryId = new SelectList(_Context.foodCategories.Where(m => m.MessId == id), "FoodCategoryId", "FoodCategoryName", food.foodCategoryId);

            return View(food);
        }


        /// <summary>
        /// return images of foods
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public JsonResult getfood()
        {
            string serverPathToChampionsFolder = "wwwroot/images/";

            //All food table record
            var ad = _Context.food.ToList();

            for (int i = 0; i < ad.Count; i++)
            {
                ad[i].photopath = serverPathToChampionsFolder + ad[i].photopath;
            }




            var json = JsonConvert.SerializeObject(ad);





            return Json(json);
        }



        //list of food
        public IActionResult FoodList()
        {
            var userinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));
            int id = userinfo.MessId;
            var food = _Context.food.Include(c => c.foodCategory).Where(m => m.foodCategory.MessId == id).ToList();
            if (food.ToList().Count == 0)
            {
                return View("Empty");
            }
            return View(food);
        }


        /// <summary>
        ///return edit view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditView(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            //photopath
            var food = await _Context.food.FindAsync(id);

            TempData["pic"] = food.photopath;
            if (food == null)
            {
                return NotFound();
            }
            //IFormFile

            Foodviewmode newfood = new Foodviewmode
            {
                FoodId = food.FoodId,
                foodName = food.FoodName,
                foodCalories = food.foodCalories,
                foodCategoryId = food.foodCategoryId,
                price = food.Price


            };
            var userinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));
            int idf = userinfo.MessId;
            ViewBag.foodCategoryId = new SelectList(_Context.foodCategories.Where(m => m.MessId == idf), "FoodCategoryId", "FoodCategoryName");
            return View(newfood);
        }
        /// <summary>
        /// edit food
        /// </summary>
        /// <param name="id"></param>
        /// <param name="food"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditView(int id, Foodviewmode food)
        {

            string uniqueFileName;
            var userinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));
            int idd = userinfo.MessId;
            if (id != food.FoodId)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {

                if (food.photo == null)
                {
                    uniqueFileName = TempData["pic"].ToString();
                }
                else
                {
                    uniqueFileName = Photopathfood(food);
                }

                // Photopathfood(food);

                //   photo = TempData["pic"].ToString();
                Food newfood1 = new Food
                {
                    FoodId = food.FoodId,
                    FoodName = food.foodName,
                    foodCalories = food.foodCalories,
                    photopath = uniqueFileName,
                    foodCategoryId = food.foodCategoryId,
                    Price = food.price


                };











                _Context.Update(newfood1);
                await _Context.SaveChangesAsync();

                return RedirectToAction(nameof(FoodList));
            }
            ViewBag.foodCategoryId = new SelectList(_Context.foodCategories.Where(m => m.MessId == idd), "FoodCategoryId", "FoodCategoryName");
            return View(food);




        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var food = _Context.food.FirstOrDefault(m => m.FoodId == id);
                _Context.food.Remove(food);
                _Context.SaveChanges();
                return RedirectToAction(nameof(FoodList));
            }

            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _Context.food
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }


        public IActionResult FoodCategory()
        {

            return View();
        }
        [HttpPost]
        public IActionResult FoodCategory(FoodCategory foodcategory)
        {
            _Context.foodCategories.Add(foodcategory);
            _Context.SaveChanges();
            return View();
        }
        public IActionResult FoodCategoryList()
        {

            return View(_Context.foodCategories.ToList());
        }

        /// <summary>
        /// return photopath
        /// </summary>
        /// <param name="food"></param>
        /// <returns>photopath  </returns>

        public string Photopathfood(Foodviewmode food)
        {

            string uniqueFileName = null;
            string paths = null;
            if (food.photo != null)
            {
                string uploadFolder = Path.Combine(HostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + food.photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                food.photo.CopyTo(new FileStream(filePath, FileMode.Create));

            }
         ;
            return uniqueFileName;
        }
        public IActionResult FoodListGenerator()
        {
            return View();
        }

        public IActionResult modelss()
        {
            return View(_Context.food.Include(c => c.foodCategory).ToList());
        }
    }
}