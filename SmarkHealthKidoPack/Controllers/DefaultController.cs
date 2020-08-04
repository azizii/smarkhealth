using System;
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
    public class DefaultController : Controller
    {

        private readonly MainContext _Context;


        public DefaultController(MainContext context)
        {
            _Context = context;

        }
        public async Task<IActionResult> LogInmanual()
        {

            //var Messinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));


            //messIdTemp = Messinfo.MessId;


            ViewData["Test"] = false;
            //device_close();
            //init_device();
            //device_open();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogInmanual(Child child)
        {
            if (child == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    Child c = _Context.children.Include(m => m.Guardian).FirstOrDefault(m => m.ChildName == child.ChildName && m.password == child.password);
                    TempData["id"] = c.ChildId;

                    if (c != null)
                    {

                        var Messinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));


                        int id = Messinfo.MessId;
                        // messIdTemp = id;
                        if (c.Guardian.messId == id)
                        {
                            // Child childS = _Context.children.Find(c.ChildId);
                            //  HttpContext.Session.Clear();
                            TempData["childid"] = c.ChildId;
                            TempData["Amountnow"] = c.Guardian.Balance;
                            TempData["guardiansalarypermanent"] = c.Guardian.Balance;
                            // HttpContext.Session.SetString("childsession", Newtonsoft.Json.JsonConvert.SerializeObject(c));
                            return RedirectToAction(nameof(Lndex));
                        }
                        else
                        {
                            ViewBag.message = "user is not register yet";
                            //ViewData["Test"] = true;
                        }
                    }
                } catch (Exception ex)
                {

                }

                ViewData["Test"] = true;
            }
            return View();
        }

        public IActionResult Lndex(string message)
        {
            int totalbalance1 = 0;

                //  device_close();
            if (message != null)
                {

                    List<ProductViewModel> cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");

                    // cart.Clear();
                    ViewBag.messages = message;
                }

                int childid = Convert.ToInt32(TempData["id"].ToString());
                DateTime da = DateTime.Now;
                //  && c1.dateselected == d
                string d = da.ToShortDateString();
                List<childfoodviewmodel> cvm = _Context.childfoodviewmodels.Where(c1 => c1.childid == childid).ToList();
                int cid = Convert.ToInt32(TempData["id"].ToString());
                Child childname = _Context.children.FirstOrDefault(m => m.ChildId == cid);
                //    var c = new List<selectedfoodviewmodel>();
                var vm = new List<selectedfoodviewmodel>();
                //   guardianname = childname.Guardian.GuardianName,
                for (int i = 0; i < cvm.Count; i++)
                {

                    vm.Add(new selectedfoodviewmodel
                    {
                        childname = childname.ChildName,
                        foodlist = _Context.food.Find(cvm[i].foodid)
                    });
                    // childname = childname.ChildName,
                    //   foodlist = _Context.food.Find(cvm[i].foodid)

                }
                if (TempData["status12345s1"] != null)
                {
                    var cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
                 totalbalance1 = Convert.ToInt32(TempData["guardiansalarypermanent"].ToString());
                decimal foodselectedprice= cart.Sum(item => item.food.Price * item.quantity);
                int newbalance = totalbalance1 - Convert.ToInt32(foodselectedprice);
                TempData["Amountnow"] = newbalance;
                ViewBag.cart = cart;
                ViewBag.total = foodselectedprice;
                }
                ViewBag.childname = childname.ChildName;
                int AmountNow = Convert.ToInt32(TempData["Amountnow"].ToString());
            if (TempData["lowbalance"] != null)
            {
                ViewBag.lowbalance = "your balance is low";
            }
                ViewBag.balance = AmountNow;
                TempData["id"] = childname.ChildId;
                TempData["Amountnow"] = AmountNow;
                TempData["status12345s"] = 1;
            ////}
               
            //TempData["guardiansalarypermanent"]
            return View(vm);
        }
        public int checlbalance(int a)
        {
            if (a<0)
            {
                return -1;
            }
            return 0;  
        }
        public IActionResult display(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var del = _Context.Registers.FirstOrDefault(m => m.RegisterId == id);
            //Register r = new Register
            //{
            //    RegisterId = del.RegisterId,
            //    finerprints = del.finerprints
            //};

            return View();
        }

        public ActionResult AddToCart(int id)
        {
            List<ProductViewModel> cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
            int a=   Convert.ToInt32(TempData["Amountnow"].ToString());
         Food f   =_Context.food.Find(id);
            if (a >= f.Price)
            {
                if (SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart") == null)
                {
                    cart = new List<ProductViewModel>();


                    cart.Add(new ProductViewModel { food = _Context.food.Find(id), quantity = 1 });
                    if (cart.Count > 0)
                    {

                        TempData["status12345s1"] = 1;

                    }

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);


                }
                else
                {
                    cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
                    int index = isExist(id);
                    if (index != -1)
                    {
                        cart[index].quantity++;
                    }
                    else
                    {
                        cart.Add(new ProductViewModel { food = _Context.food.Find(id), quantity = 1 });



                    }
                  
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }



            }
            else
            {
                TempData["lowbalance"] = 123;
            }


            if (cart.Count > 0)
            {

                TempData["status12345s1"] = 1;


            }

            int childid = Convert.ToInt32(TempData["childid"].ToString());
            TempData["id"] = childid;
            // Child userinfo = JsonConvert.DeserializeObject<Child>(HttpContext.Session.GetString("Childs"));
            // int qid = childid;
            return RedirectToAction("Lndex");
        }


        public IActionResult showcart()
        {

            var cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.food.Price * item.quantity);
            return View();
        }
        /// <summary>
        /// save all selected item from kids
        /// </summary>
        /// <returns></returns>
        public IActionResult Save()
        {

            //   Child child = JsonConvert.DeserializeObject<Child>(HttpContext.Session.GetString("childsession"));
            // qid = child.ChildId;

            int kidId = Convert.ToInt32(TempData["id"].ToString());
            //  int childid = Convert.ToInt32(TempData["ChildId"].ToString());
            DateTime da = DateTime.Now;
            //  && c1.dateselected == d
            string d = da.ToShortDateString();

            List<ProductViewModel> cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            decimal total= cart.Sum(item => item.food.Price * item.quantity);
            ViewBag.total = cart.Sum(item => item.food.Price * item.quantity);
            //find child
            Child child = _Context.children.Find(kidId);
            ////find guardain
            int guardainid = child.guardianId;
            Guardian guardain = _Context.guardians.Find(guardainid);
            int databaseorignalvalue = guardain.Balance;
            int newvalue = databaseorignalvalue - Convert.ToInt32(total);
            guardain.Balance = newvalue;
            _Context.guardians.Update(guardain);
            _Context.SaveChanges();
            var c = new List<SelectedChildfoods>();
            for (int i = 0; i < cart.Count; i++)
            {
                c.Add(new SelectedChildfoods
                {
                    foodid = cart[i].food.FoodId,
                    childid = kidId,
                    dateselected = d,
                    quantity = cart[i].quantity

                });
       
            
            }

            foreach (SelectedChildfoods employee in c)
            {
                _Context.Add(employee);
            }

         
            for (int j = 0; j < cart.Count; j++)
            {
                cart.RemoveAt(j);
            }
           // SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart").Clear();
            SessionHelper.removesession(HttpContext.Session, "cart", cart);
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

            //   TempData["status1234"] = 1;

            _Context.SaveChanges();
            //   
            return View();
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
            if (index != -1)
            {
                cart.RemoveAt(index);
            }
            int total = cart.Count;
            if (total != 0)
            {
                TempData["status12345s1"] = 1;
            }
            TempData["status12345s1"] = 1;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Lndex");
        }
    }
}