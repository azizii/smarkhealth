using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmarkHealthKidoPack.Models;

namespace SmarkHealthKidoPack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Foods1Controller : ControllerBase
    {
        private readonly MainContext _context;

        public Foods1Controller(MainContext context)
        {
            _context = context;
        }

        // GET: api/Foods1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> Getfood()
        {
            string serverPathTowwwrootFolder = "wwwroot/images/";
            //All food table record
            var ad = _context.food.ToList();

            for (int i = 0; i < ad.Count; i++)
            {
                ad[i].photopath = serverPathTowwwrootFolder + ad[i].photopath;
            }

            return  ad;
        }
      
        // GET: api/Foods1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            var food = await _context.food.FindAsync(id);

            if (food == null)
            {
                return NotFound();
            }

            return food;
        }

        // PUT: api/Foods1/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutFood(int id, Food food)
        //{
        //    if (id != food.FoodId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(food).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FoodExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Foods1
        //[HttpPost]
        //public async Task<ActionResult<Food>> PostFood(Food food)
        //{
        //    _context.food.Add(food);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetFood", new { id = food.FoodId }, food);
        //}

        //// DELETE: api/Foods1/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Food>> DeleteFood(int id)
        //{
        //    var food = await _context.food.FindAsync(id);
        //    if (food == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.food.Remove(food);
        //    await _context.SaveChangesAsync();

        //    return food;
        //}

        //private bool FoodExists(int id)
        //{
        //    return _context.food.Any(e => e.FoodId == id);
        //}
    }
}
