using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmarkHealthKidoPack.Models;
using SmarkHealthKidoPack.ViewModel;

namespace SmarkHealthKidoPack.Controllers
{
    public class SelectedFoodController : Controller
    {
        private readonly MainContext _Context;
        public SelectedFoodController(MainContext mainContext)
        {
            _Context = mainContext;
        }
        public IActionResult Index(int? id)
        {
         List<childfoodviewmodel>  c= _Context.childfoodviewmodels.Where(x => x.childid == id).ToList();
         
            Child child = _Context.children.Find(id);
            var guardainseechildselectedFoodList = new List<GuardainSeeChildselectedFood>();
            for (int i=0; i<c.Count; i++)
            {

                guardainseechildselectedFoodList.Add(new GuardainSeeChildselectedFood {foods=_Context.food.Find(c[i].foodid),selecteddate=c[i].dateselected,childname= child.ChildName,quantity=0 } );
            }
            //foreach (GuardainSeeChildselectedFood selected in guardainseechildselectedFoodList)
            //{

            //}
            return View(guardainseechildselectedFoodList);
        }
        public ActionResult purcahsedfood(int? id)
        {
            List<SelectedChildfoods> c=_Context.selectedChildfoodss.Where(x => x.childid == id).ToList();

            Child child = _Context.children.Find(id);
            var guardainseechildselectedFoodList = new List<GuardainSeeChildselectedFood>();
            for (int i = 0; i < c.Count; i++)
            {

                guardainseechildselectedFoodList.Add(new GuardainSeeChildselectedFood { foods = _Context.food.Find(c[i].foodid), selecteddate = c[i].dateselected, childname = child.ChildName, quantity = c[i].quantity });
            }
            return View(guardainseechildselectedFoodList);
        }
    }
}