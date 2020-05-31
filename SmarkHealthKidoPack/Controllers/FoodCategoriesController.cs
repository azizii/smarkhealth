using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmarkHealthKidoPack.Models;
using SmarkHealthKidoPack.ViewModel;

namespace SmarkHealthKidoPack.Controllers
{
    public class FoodCategoriesController : Controller
    {
        private readonly MainContext _context;

        public FoodCategoriesController(MainContext context)
        {
            _context = context;
        }

        // GET: FoodCategories
        public async Task<IActionResult> Index()
        {
            var userinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));
            int id = userinfo.MessId;
            var foodcategory = await _context.foodCategories.Where(m => m.MessId == id).ToListAsync();
            if (foodcategory.ToList().Count == 0)
            {
                return View("Empty");
            }
            return View(foodcategory);
        }

        // GET: FoodCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodCategory = await _context.foodCategories
                .FirstOrDefaultAsync(m => m.FoodCategoryId == id);
            if (foodCategory == null)
            {
                return NotFound();
            }

            return View(foodCategory);
        }

        // GET: FoodCategories/Create
        public IActionResult Create()
        {
            var viewModel = new FoodCategoryViewModel
            { Referer = Request.Headers["Referer"].ToString() };
            return View(viewModel);
        }

        // POST: FoodCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodCategoryViewModel  foodCategoryViewModel)
        {
            var userinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));
            int id = userinfo.MessId;

            if (ModelState.IsValid)
            {

                FoodCategory fc = new FoodCategory
                {
                    FoodCategoryName = foodCategoryViewModel.foodCategory.FoodCategoryName,
                    MessId = id
                };

                _context.Add(fc);
                await _context.SaveChangesAsync();


                if (!String.IsNullOrEmpty(foodCategoryViewModel.Referer))
                {
                    return Redirect(foodCategoryViewModel.Referer);
                }
                //   return RedirectToAction(nameof(Index));
            }
            return View(foodCategoryViewModel);
        }

        // GET: FoodCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodCategory = await _context.foodCategories.FindAsync(id);
            if (foodCategory == null)
            {
                return NotFound();
            }
            return View(foodCategory);
        }

        // POST: FoodCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodCategoryId,FoodCategoryName")] FoodCategory foodCategory)
        {
            if (id != foodCategory.FoodCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodCategoryExists(foodCategory.FoodCategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(foodCategory);
        }

        // GET: FoodCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodCategory = await _context.foodCategories
                .FirstOrDefaultAsync(m => m.FoodCategoryId == id);
            if (foodCategory == null)
            {
                return NotFound();
            }

            return View(foodCategory);
        }

        // POST: FoodCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodCategory = await _context.foodCategories.FindAsync(id);
            _context.foodCategories.Remove(foodCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodCategoryExists(int id)
        {
            return _context.foodCategories.Any(e => e.FoodCategoryId == id);
        }
    }
}
