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
    public class GuardiansController : Controller
    {
        private readonly MainContext _context;

        public GuardiansController(MainContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get all messes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mess>>> GetMesses()
        {
           // string serverPathTowwwrootFolder = "wwwroot/images/";
            //All food table record
            var ad = _context.Messes.ToList();

          

            return ad;
        }
        /// <summary>
        /// register guardianthroug api
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostRegister([FromBody]Guardian value)
        {
            if (!_context.guardians.Any(user => user.GuardianName.Equals(value.GuardianName)))
            {
                Guardian tblUser = new Guardian();
                tblUser.GuardianName = value.GuardianName;
                tblUser.phonenumber = value.phonenumber;
                tblUser.passward = value.passward;
                tblUser.messId = value.messId;
                try
                {
                    _context.Add(tblUser);
                    _context.SaveChanges();
                    return JsonConvert.SerializeObject("register succefully");
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(ex.Message);
                }
            }
            else
            {
                return JsonConvert.SerializeObject("User is existing in database");
            }
        }


        public async Task<IActionResult> Register()
        {
            ViewBag.messId = new SelectList(_context.Set<Mess>(), "MessId", "MessName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(GuardianViewMode guardian)
        {

            if (ModelState.IsValid)
            {


                Guardian g1 = new Guardian
                {
                    GuardianName = guardian.GuardianName,
                    phonenumber = guardian.phonenumber,
                    adress = guardian.adress,
                    passward = guardian.passward,
                    messId=guardian.MessId
             
                };



                _context.Add(g1);

                _context.SaveChanges();

                return RedirectToAction(nameof(LogIn));
            }
            ViewBag.messId = new SelectList(_context.Set<Mess>(), "MessId", "MessName ");
            return View(guardian);
        }


        /// <summary>
        /// log in api guardian
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public string PostlogIn([FromBody]Guardian value)
        {
           
                Guardian guar = _context.guardians
                 .FirstOrDefault(m => m.GuardianName == value.GuardianName && m.passward == value.passward);
                if(guar != null)
                {
                HttpContext.Session.SetString("Guardian", Newtonsoft.Json.JsonConvert.SerializeObject(guar));
                return JsonConvert.SerializeObject("LogIn succefully");
            }
            else
            {
                return JsonConvert.SerializeObject("LgIn not succefull");
            }
            
            //var guardianobj = _context.guardians
            //   .FirstOrDefault(m => m.GuardianName == value.GuardianName && m.passward == value.passward);
            //if (guardianobj != null)/* && passward !=null*/
            //{
          
              
             
            //}
            //else
            //{
            //    return JsonConvert.SerializeObject("User is existing in database");
            //}
         }
        public async Task<IActionResult> LogIn()
        {
            if (HttpContext.Session.GetString("Guardian") != null)
            {
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["Test"] = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(Guardian m1)
        {

            var guardianobj = _context.guardians
                 .FirstOrDefault(m => m.GuardianName == m1.GuardianName && m.passward == m1.passward);
            // var mess = _context.Mess
            //.Where(l => l.MessName == m.MessName && l.Password == m.Password);

            if (guardianobj != null)/* && passward !=null*/
            {
                //create session
                HttpContext.Session.SetString("Guardian", Newtonsoft.Json.JsonConvert.SerializeObject(guardianobj));
                Guardian userinfo = JsonConvert.DeserializeObject<Guardian>(HttpContext.Session.GetString("Guardian"));
                TempData["messid"] = userinfo.messId;
                //  HttpContext.Session.SetString("sessionUser", Newtonsoft.Json.JsonConvert.SerializeObject(mess));
                return RedirectToAction(nameof(Index));
            }
            ViewData["Test"] = true;
            return View();
        }

        /// <summary>
        /// get all food api
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///      var food = _context.food.Include(c => c.foodCategory).Where(m => m.foodCategory.MessId == qid).ToList();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> Getfood()
        {
            Guardian userinfo = JsonConvert.DeserializeObject<Guardian>(HttpContext.Session.GetString("Guardian"));
             var food = _context.food.Include(c1 => c1.foodCategory).Where(m => m.foodCategory.MessId == userinfo.messId).ToList();
         
            string serverPathTowwwrootFolder = "images/";
            var ad = _context.food.ToList();
            //All food table record
            //     List<Food> ad = _context.food.ToList();

            var c = new List<Food>();
            for (int i = 0; i < food.Count; i++)
            {
                c.Add(new Food
                {
                    FoodId = food[i].FoodId,
                    FoodName= food[i].FoodName,
                    Price= food[i].Price,
                    foodCalories= food[i].foodCalories,
                    foodCategoryId= food[i].foodCategoryId

                });

                //if (cart[i].food.FoodId.Equals())
                //{
                //    return i;
                //}
            }


            for (int i = 0; i < c.Count; i++)
            {
                c[i].photopath = serverPathTowwwrootFolder + ad[i].photopath;
            }

            return c;
        }
        public async Task<IActionResult> FoodList(int? id)
        {


            if (id !=null)
            {
           Child child=     _context.children.Find(id);
                HttpContext.Session.SetString("Child", Newtonsoft.Json.JsonConvert.SerializeObject(child));
                //TempData["ChildId"] = child.ChildId;
                ViewBag.chid = " select food for " + child.ChildName;


            }
           // qid = Convert.ToInt32(TempData["messid"].ToString());
            if (TempData["status"] != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.food.Price * item.quantity);
            }

            int qid = 0;
            //var userinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));
            //int id = userinfo.MessId;

            if (TempData["messid"]==null)
            {
                Guardian userinfo = JsonConvert.DeserializeObject<Guardian>(HttpContext.Session.GetString("Guardian"));
                TempData["messid"] = userinfo.messId;
                qid = Convert.ToInt32(TempData["messid"].ToString());
            }
            else
            {
                qid = Convert.ToInt32(TempData["messid"].ToString());
            }
           // var food = _context.food.Include(c => c.foodCategory).Include(c => c.Mess).Where(c => c.MessId == qid).ToList();
            var food = _context.food.Include(c => c.foodCategory).Where(m => m.foodCategory.MessId == qid).ToList();
            if (food.ToList().Count == 0)
            {
                return View("Empty");
            }
            return View(food);
        }
        // GET: Guardians
        public async Task<IActionResult> Index()
        {
            Guardian userinfo = JsonConvert.DeserializeObject<Guardian>(HttpContext.Session.GetString("Guardian"));

            Guardian guardian1 = _context.guardians.FirstOrDefault(m => m.GuardianId == userinfo.GuardianId);

            List<Child> childs = _context.children.Where(m => m.guardianId == userinfo.GuardianId).ToList();

            var c = new guardianchildViewModel
            {
                guardian = guardian1,
                children = childs

            };
            //for (int i=0; i<mainContext.Count; i++)
            //{
            //    d = mainContext[i].Guardian


            //};


            //    children =



            //    };

            return View(c);
        }



        /// <summary>
        /// getchid
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Child>>>  GetChild()
        {
            Guardian userinfo = JsonConvert.DeserializeObject<Guardian>(HttpContext.Session.GetString("Guardian"));

            Guardian guardian1 = _context.guardians.FirstOrDefault(m => m.GuardianId == userinfo.GuardianId);

            List<Child> childs = _context.children.Where(m => m.guardianId == userinfo.GuardianId).ToList();
            //All food table record


            var c = new List<Child>();
            for (int i = 0; i < childs.Count; i++)
            {
                c.Add(new Child
                {
                    ChildId = childs[i].ChildId,
                    ChildName = childs[i].ChildName,
                   

                });

                //if (cart[i].food.FoodId.Equals())
                //{
                //    return i;
                //}
            }





            return c;
        }


        /// <summary>
        /// delete child
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       
        
        
        public async Task<IActionResult> deletechild(int? id)
        {
            var childid= await _context.children.FindAsync(id);
            _context.children.Remove(childid);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// register child
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public string PostRegisterChild([FromBody]Child child)
        {

            var userinfo = JsonConvert.DeserializeObject<Guardian>(HttpContext.Session.GetString("Guardian"));
            int id = userinfo.GuardianId;

            Guardian g = _context.guardians.FirstOrDefault(M => M.GuardianId == id);
            Child c = new Child
            {
                ChildName = child.ChildName,
                age = child.age,
                height = child.height,
                weight = child.weight,
                password = child.password,
                guardianId = id

            };
           
            try
                {
                _context.Add(c);

                _context.SaveChanges();
                return JsonConvert.SerializeObject("register succefully");
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(ex.Message);
                }
            
         
        }
        public async Task<IActionResult> ChildRegister()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChildRegister(Child child)
        {
            if (ModelState.IsValid)
            {
                var userinfo = JsonConvert.DeserializeObject<Guardian>(HttpContext.Session.GetString("Guardian"));
                int id = userinfo.GuardianId;

                Guardian g = _context.guardians.FirstOrDefault(M => M.GuardianId == id);
                Child c = new Child
                {
                    ChildName = child.ChildName,
                    age = child.age,
                    height = child.height,
                    weight = child.weight,
                    password = child.password,
                    guardianId = id

                };
                _context.Add(c);

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
  
            return View(child);
        }
        // GET: Guardians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardian = await _context.guardians
                .Include(g => g.Mess)
                .FirstOrDefaultAsync(m => m.GuardianId == id);
            if (guardian == null)
            {
                return NotFound();
            }

            return View(guardian);
        }

        // GET: Guardians/Create
        public IActionResult Create()
        {
            ViewData["MessId"] = new SelectList(_context.Messes, "MessId", "MessName");
            return View();
        }

        // POST: Guardians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuardianId,GuardianName,phonenumber,adress,passward,MessId")] Guardian guardian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guardian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MessId"] = new SelectList(_context.Messes, "MessId", "MessName", guardian.messId);
            return View(guardian);
        }

        // GET: Guardians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foods = await _context.food.FindAsync(id);
            if (foods == null)
            {
                return NotFound();
            }

         int   childid = Convert.ToInt32(TempData["ChildId"].ToString());
            //SelectedFoodItems s = new SelectedFoodItems {
            //    SelectedFoodItemsId=1,
            //    FoodId = foods.FoodId,
            //    MessId = foods.MessId,
            //    KidsId = childid,
            //    FoodName = foods.FoodName,
            //    MessName = foods.Mess.MessName,
            //    Quantity = 1
            //   };
            //_context.Add(s);
            //await _context.SaveChangesAsync();

            ViewData["MessId"] = new SelectList(_context.Messes, "MessId", "MessName", foods.foodCategoryId);
            return View(foods);
        }

        // POST: Guardians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       

        // GET: Guardians/Delete/5
        //public async Task<IActionResult> selectfood(int id)
        //{



        //    if (id == null)
        //    {
        //        return NotFound();
        //    }


        //    int childid = Convert.ToInt32(TempData["ChildId"].ToString());
        //    var food= await _context.food
                
        //        .FirstOrDefaultAsync(m => m.FoodId == id);
        //    var child = await _context.children

        //       .FirstOrDefaultAsync(m => m.ChildId == childid);
        //    if (food == null)
        //    {
        //        return NotFound();
        //    }


        //    int a = food.MessId;
 
        //    ChildFood c = new ChildFood
        //    {
        //        ChildId= childid,
        //        FoodId= id
        //    };

        //    _context.Add(c);
        //    _context.SaveChanges();

        //    var all = _context.childFoods.ToList();


        //    TempData["ChildId"] = child.ChildId;
        //    return View(all);
        //}


            public ActionResult AddToCart(int id)
        {


            if (SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart") == null)
            {
                List<ProductViewModel> cart = new List<ProductViewModel>();

                
                cart.Add(new ProductViewModel{ food = _context.food.Find(id), quantity = 1 });
               if (cart.Count > 0)
                {

                    TempData["status"] = 1;
                    
                  //  TempData["status"] = "1";
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
                    cart.Add(new ProductViewModel { food = _context.food.Find(id), quantity = 1 });

                    

                }

                if (cart.Count > 0)
                {

                    TempData["status"] = 1;

                    //  TempData["status"] = "1";
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }




            //List<ProductViewModel> cart = new List<ProductViewModel>();
            //var product = _context.food

            //.Find(id);

            //cart.Add(new ProductViewModel()
            //{
            //    food= product,
            //    quantity=1
            //}
            //    );

            //HttpContext.Session.SetString("cart", Newtonsoft.Json.JsonConvert.SerializeObject(cart));

            ////   HttpContext.Session.SetComplexData("cart", cart);
            ////   TempData["cart"] = JsonConvert.SerializeObject(cart);
            ////ViewData["PopupMessages"] = JsonConvert.DeserializeObject<List<PopupMessage>>(TempData["PopupMessages"]);

            Child userinfo = JsonConvert.DeserializeObject<Child>(HttpContext.Session.GetString("Child"));
            int qid = userinfo.ChildId;
            return RedirectToAction("FoodList", new { id = qid }); 
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
            Guardian userinfo = JsonConvert.DeserializeObject<Guardian>(HttpContext.Session.GetString("Guardian"));

            Child userinfo1 = JsonConvert.DeserializeObject<Child>(HttpContext.Session.GetString("Child"));
            int qid = userinfo1.ChildId;

          //  int childid = Convert.ToInt32(TempData["ChildId"].ToString());

            List<ProductViewModel> cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
           var c = new List<childfoodviewmodel>();
            for (int i = 0; i < cart.Count; i++)
            {
                c.Add(new childfoodviewmodel
                {
                     foodid=cart[i].food.FoodId,
                    childid= qid

                });
        
                //if (cart[i].food.FoodId.Equals())
                //{
                //    return i;
                //}
            }

            foreach (childfoodviewmodel employee in c)
            {
                _context.childfoodviewmodels.Add(employee);
            }


   
            _context.SaveChanges();
           
            return RedirectToAction("Index");
        }


        /// <summary>
        /// post method for selected  food for child
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostChildfood([FromBody]int[] childfood)
        {
            Food food = null;
            int childid = childfood[0];
            var foods = new List<Food>();

            for (int i=1; i<childfood.Length; i++)
            {
                foods.Add(food = _context.food.Find(childfood[i]));
            }


            if (foods.Count != 0)
            {
              //  var c = new List<childfoodviewmodel>();
                for (int i = 0; i < foods.Count; i++)
                {
                    childfoodviewmodel c = new childfoodviewmodel {
                        foodid = foods[i].FoodId,
                       childid= childid
                    };

                    _context.Add(c);
                    //c.Add(new childfoodviewmodel
                    //{
                    //    foodid = foods[i].FoodId,
                    //    childid = childid

                    //});

                    //if (cart[i].food.FoodId.Equals())
                    //{
                    //    return i;
                    //}
                }

                //    try
                //   {
                //foreach (childfoodviewmodel employee in c)
                //{
                //    _context.Add(employee);
                //}

                _context.SaveChanges();
                return JsonConvert.SerializeObject("food save succefully");
            }
            else
            {
                return JsonConvert.SerializeObject("please select food ");
            }

       
          ////  }
        //    catch (Exception ex)
          //  {
           //     return JsonConvert.SerializeObject(ex.Message);
           // }


        }


        public IActionResult Remove(int id)
        {
            List<ProductViewModel> cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("showcart");
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

        // POST: Guardians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guardian = await _context.guardians.FindAsync(id);
            _context.guardians.Remove(guardian);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult LogOut()
        {
            //clear session
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(LogIn));
            //return View("LogIn");
        }
    }
}
