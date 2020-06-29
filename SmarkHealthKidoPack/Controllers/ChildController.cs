﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmarkHealthKidoPack.Models;
using SmarkHealthKidoPack.ViewModel;

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

                Child c = _Context.children.Include(m =>m.Guardian).FirstOrDefault(m => m.ChildName == child.ChildName && m.password == child.password);

                TempData["id"] = c.ChildId;
                // 
                if (c!=null)
                {

                    var Messinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));


                    int id = Messinfo.MessId;
                    if (c.Guardian.messId == id) {
                        // Child childS = _Context.children.Find(c.ChildId);
                        TempData["childid"] = c.ChildId;
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
            List<childfoodviewmodel> cvm= _Context.childfoodviewmodels.Where(c1 => c1.childid == childid).ToList();
        int    cid = Convert.ToInt32(TempData["id"].ToString());
            Child childname = _Context.children.FirstOrDefault(m => m.ChildId == cid);
        //    var c = new List<selectedfoodviewmodel>();
            var vm = new List<selectedfoodviewmodel>();
         //   guardianname = childname.Guardian.GuardianName,
            for (int i=0; i<cvm.Count; i++)
            {

                vm.Add(new selectedfoodviewmodel
                {
                    childname= childname.ChildName,
                    foodlist=_Context.food.Find(cvm[i].foodid)
                });
                   // childname = childname.ChildName,
                 //   foodlist = _Context.food.Find(cvm[i].foodid)
             
            }
            if (TempData["status12"] !=null)
            {
                 var cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.food.Price * item.quantity);
            }
            TempData["id"] = childname.ChildId;
            //cvm[0].foodi
            return View(vm);
        }

        public ActionResult AddToCart(int id)
        {


            if (SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart") == null)
            {
                List<ProductViewModel> cart = new List<ProductViewModel>();


                cart.Add(new ProductViewModel { food = _Context.food.Find(id), quantity = 1 });
                if (cart.Count > 0)
                {

                    TempData["status12"] = 1;

                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);


            }
            else
            {
                List<ProductViewModel> cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].quantity++;
                }
                else
                {
                    cart.Add(new ProductViewModel { food = _Context.food.Find(id), quantity = 1 });



                }

                if (cart.Count > 0)
                {

                    TempData["status12"] = 1;

                   
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }



            int childid = Convert.ToInt32(TempData["childid"].ToString());
            TempData["id"] = childid;
           // Child userinfo = JsonConvert.DeserializeObject<Child>(HttpContext.Session.GetString("Childs"));
           // int qid = childid;
            return RedirectToAction("Index");
        }


        public IActionResult showcart()
        {

            var cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.food.Price * item.quantity);
            return View();
        }

        public IActionResult Save()
        {
            //Guardian userinfo = JsonConvert.DeserializeObject<Guardian>(HttpContext.Session.GetString("Guardian"));

            //Child userinfo1 = JsonConvert.DeserializeObject<Child>(HttpContext.Session.GetString("Child"));
            //int qid = userinfo1.ChildId;

            ////  int childid = Convert.ToInt32(TempData["ChildId"].ToString());

            //List<ProductViewModel> cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
            //var c = new List<childfoodviewmodel>();
            //for (int i = 0; i < cart.Count; i++)
            //{
            //    c.Add(new childfoodviewmodel
            //    {
            //        foodid = cart[i].food.FoodId,
            //        childid = qid

            //    });

            //    //if (cart[i].food.FoodId.Equals())
            //    //{
            //    //    return i;
            //    //}
            //}

            //foreach (childfoodviewmodel employee in c)
            //{
            //    _Context.childfoodviewmodels.Add(employee);
            //}



            //_Context.SaveChanges();

            return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            List<ProductViewModel> cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].food.FoodId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        public IActionResult Remove(int id)
        {
            List<ProductViewModel> cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        //public IActionResult showcart()
        //{

        //    var cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
        //    ViewBag.cart = cart;
        //    ViewBag.total = cart.Sum(item => item.food.Price * item.quantity);
        //    return View();
        //}
    }


}
