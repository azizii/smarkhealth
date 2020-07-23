using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libzkfpcsharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmarkHealthKidoPack.Models;
using SmarkHealthKidoPack.ViewModel;


using libzkfpcsharp;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using System.Threading;
using System.Data;

namespace SmarkHealthKidoPack.Controllers
{
    public class ChildController : Controller
    {
        public String Value = string.Empty;
        public int id = 10;
        public readonly MainContext _Context;

        zkfp fpInstance = new zkfp();


        IntPtr mDevHandle = IntPtr.Zero;
        IntPtr mDBHandle = IntPtr.Zero;
        IntPtr FormHandle = IntPtr.Zero;
        bool bIsTimeToDie = false;
        bool IsRegister = false;
        bool bIdentify = true;
        byte[] FPBuffer;
        int RegisterCount = 0;
        const int REGISTER_FINGER_COUNT = 3;

        byte[][] RegTmps = new byte[3][];
        byte[] RegTmp = new byte[2048];
        byte[] CapTmp = new byte[2048];
        int cbCapTmp = 2048;
        int cbRegTmp = 0;
        int iFid = 1;
        public string db_value;
        private int mfpWidth = 0;
        private int mfpHeight = 0;
        Message message1 = new Message();
        const int MESSAGE_CAPTURED_OK = 0x0400 + 6;
        // message1 = 0x002b0812;
        const int message2 = 1030;
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);


        int messIdTemp = 0;
        string childIdTemp = "";
       
        public ChildController(MainContext context)
        {
            _Context = context;


        }

        public IActionResult LogIn()
        {

            var Messinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));


            messIdTemp = Messinfo.MessId;


            ViewData["Test"] = false;
            device_close();
            init_device();
            device_open();
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(int child)
        {
            fingerdata d = null;
            try
            {
                d = _Context.fingerdatas.First();
            }
            catch (Exception ex)
            {

            }
           
            if (child == null)
            {
                return NotFound();
            }

           
                try
                {
                    Child c = _Context.children.Include(m => m.Guardian).FirstOrDefault(m => m.ChildId == d.finger);

                    TempData["id"] = c.ChildId;
                    // 
                    if (c != null)
                    {

                        var Messinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));


                        int id = Messinfo.MessId;
                        messIdTemp = id;
                        if (c.Guardian.messId == id)
                        {
                            // Child childS = _Context.children.Find(c.ChildId);
                            //  HttpContext.Session.Clear();
                            TempData["childid"] = c.ChildId;
                            TempData["guardiansalary"] = c.Guardian.Balance;
                        _Context.Database.ExecuteSqlCommand("TRUNCATE TABLE [fingerdatas]");
                        // HttpContext.Session.SetString("childsession", Newtonsoft.Json.JsonConvert.SerializeObject(c));
                        return RedirectToAction(nameof(Lndex));
                        }
                        else
                        {
                            ViewBag.message = "user is not register yet";
                            //ViewData["Test"] = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewData["Test"] = true;
                }
                ViewData["Test"] = true;

            
            return View();
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

                Child c = _Context.children.Include(m => m.Guardian).FirstOrDefault(m => m.ChildName == child.ChildName && m.password == child.password);
                TempData["id"] = c.ChildId;
                // 
                if (c != null)
                {

                    var Messinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));


                    int id = Messinfo.MessId;
                    messIdTemp = id;
                    if (c.Guardian.messId == id)
                    {
                        // Child childS = _Context.children.Find(c.ChildId);
                        //  HttpContext.Session.Clear();
                        TempData["childid"] = c.ChildId;
                        TempData["guardiansalary"] = c.Guardian.Balance;
                        // HttpContext.Session.SetString("childsession", Newtonsoft.Json.JsonConvert.SerializeObject(c));
                        return RedirectToAction(nameof(Lndex));
                    }
                    else
                    {
                        ViewBag.message = "user is not register yet";
                        //ViewData["Test"] = true;
                    }
                }
                ViewData["Test"] = true;
            }
            return View();
        }





        [HttpPost]
        public IActionResult LogIn1(Child child)
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

                    // 
                    if (c != null)
                    {

                        var Messinfo = JsonConvert.DeserializeObject<Mess>(HttpContext.Session.GetString("sessionUser1234"));


                        int id = Messinfo.MessId;
                        messIdTemp = id;
                        if (c.Guardian.messId == id)
                        {
                            // Child childS = _Context.children.Find(c.ChildId);
                            //  HttpContext.Session.Clear();
                            TempData["childid"] = c.ChildId;
                            TempData["guardiansalary"] = c.Guardian.Balance;
                            // HttpContext.Session.SetString("childsession", Newtonsoft.Json.JsonConvert.SerializeObject(c));
                            return RedirectToAction(nameof(Lndex));
                        }
                        else
                        {
                            ViewBag.message = "user is not register yet";
                            //ViewData["Test"] = true;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                ViewData["Test"] = true;
            }
            return View("LogIn");
        }




        public IActionResult Lndex(string message)
        {
            device_close();
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
            if (TempData["status12345"] != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<ProductViewModel>>(HttpContext.Session, "cart");


                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.food.Price * item.quantity);
            }
            ViewBag.childname = childname.ChildName;
            int balance = Convert.ToInt32(TempData["guardiansalary"].ToString());
            ViewBag.balance = balance;
            TempData["id"] = childname.ChildId;
            TempData["guardiansalary"] = balance;

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

                    TempData["status12345"] = 1;

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

                    TempData["status12345"] = 1;


                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
         
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

                //if (cart[i].food.FoodId.Equals())
                //{
                //    return i;
                //}
            }

            foreach (SelectedChildfoods employee in c)
            {
                _Context.Add(employee);
            }


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
            return RedirectToAction("Lndex", new { Message = "datasave" });
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
                TempData["status12345"] = 1;
            }

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Lndex");
        }

        // Now other authentication code\


        public void init_device()
        {
            // cmbIdx.Items.Clear();
            int ret = zkfperrdef.ZKFP_ERR_OK;
            if ((ret = zkfp2.Init()) == zkfperrdef.ZKFP_ERR_OK)
            {
                int nCount = zkfp2.GetDeviceCount();
                if (nCount > 0)
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        // cmbIdx.Items.Add(i.ToString());
                    }
                    // cmbIdx.SelectedIndex = 0;
                    //bnInit.Enabled = false;
                    //bnFree.Enabled = true;
                    //bnOpen.Enabled = true;
                    Console.WriteLine("Device connected");
                }

                else
                {
                    zkfp2.Terminate();
                    // MessageBox.Show("No device connected!");
                    Console.WriteLine("No device connected");
                }
            }
            else
            {
                Console.WriteLine("Initialize fail, ret=" + ret + " !");
                // MessageBox.Show("Initialize fail, ret=" + ret + " !");
            }
        }

        private void DoCapture()
        {
            while (!bIsTimeToDie)
            {
                cbCapTmp = 2048;
                int ret = zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, CapTmp, ref cbCapTmp);

                if (ret == zkfp.ZKFP_ERR_OK)
                {
                    //SendMessage(FormHandle, MESSAGE_CAPTURED_OK, IntPtr.Zero, IntPtr.Zero);
                    // start




                    // Get all records from DB
                    string query = String.Format("select * from children c " +
                        "join guardians g on g.guardianid = c.guardianid " +
                        "join messes m on m.Messid = g.messid where m.messid = {0}", messIdTemp);
                    SqlConnection con = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Database=SmartHealth;Trusted_Connection=True;MultipleActiveResultSets=true");
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, con);
                    con.Open();
                    DataTable dt = new DataTable();
                    sqlDataAdapter.Fill(dt);

                    zkfp t = new zkfp();

                    
                    foreach (DataRow dr in dt.Rows)
                    {
                        string cFP = dr["FingerPrint"].ToString();
                        string cKId = dr["ChildId"].ToString();

                        if (!String.IsNullOrEmpty(cFP))
                        {
                            byte[] currentFingerPrint =
                                System.Convert.FromBase64String(cFP);
                            var data = zkfp2.DBMatch(mDBHandle, CapTmp, currentFingerPrint);

                            if (data > 50 && data <= 100)
                            {
                                // match successfull
                                //childIdTemp = cKId;
                                //TempData["temp"] = cKId;
                                ViewBag.temp = cKId;
                                int childids= Int16.Parse(cKId);
                       
                                device_close();
                                try
                                {

                                    add(childids);
                          
                                }
                                catch (Exception ex)
                                {

                                }
                                //Response.Redirect("~/Child/indexsave");
                                ///   RedirectToAction("indexsave").ExecuteResult(this.ControllerContext);
                               // messIdTemp = messIdTemp;

                                
                            }

                        }
                       
                    }
                   
                    con.Close();

                 


                }
                Thread.Sleep(200);
            }

          
        }

        public void device_open()
        {
            int ret = zkfp.ZKFP_ERR_OK;
            if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(0)))
            {
                Console.WriteLine("open device fail");
                // MessageBox.Show("OpenDevice fail");
                return;
            }
            if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))
            {
                Console.WriteLine("init_device DB fail");
                // MessageBox.Show("Init DB fail");
                zkfp2.CloseDevice(mDevHandle);
                mDevHandle = IntPtr.Zero;
                return;
            }
            //bnInit.Enabled = false;
            //bnFree.Enabled = true;
            //bnOpen.Enabled = false;
            //bnClose.Enabled = true;
            //bnEnroll.Enabled = true;
            //bnVerify.Enabled = true;
            //bnIdentify.Enabled = true;
            RegisterCount = 0;
            cbRegTmp = 0;
            iFid = 1;
            for (int i = 0; i < 3; i++)
            {
                RegTmps[i] = new byte[2048];
            }
            byte[] paramValue = new byte[4];
            int size = 4;
            zkfp2.GetParameters(mDevHandle, 1, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpWidth);

            size = 4;
            zkfp2.GetParameters(mDevHandle, 2, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpHeight);

            FPBuffer = new byte[mfpWidth * mfpHeight];

            Thread captureThread = new Thread(new ThreadStart(DoCapture));
            captureThread.IsBackground = true;
            captureThread.Start();
            bIsTimeToDie = false;
          
           
            //textRes.Text = "Open succ";
            Console.WriteLine("Open success");
        }

        public void device_close()
        {
            try
            {
                bIsTimeToDie = true;
                RegisterCount = 0;
                Thread.Sleep(1000);
                fpInstance.CloseDevice();
                zkfp2.CloseDevice(mDevHandle);
                Console.WriteLine("Device closed ");
            }
            catch (Exception e) { }
        }
        public void add(int a)
      {

            fingerdata c = new fingerdata {
            finger=a};
            try
            {
                string query = "INSERT INTO fingerdatas (finger) VALUES ('"+a+"')";
                SqlConnection con = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Database=SmartHealth;Trusted_Connection=True;MultipleActiveResultSets=true");
                SqlCommand cmd = new SqlCommand(query, con);
                //cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@finerprints",db_value);

                con.Open();
                int i = cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {

            }
            //try
            //{


            //    List<ChildEnrolmentViewModel> carts = new List<ChildEnrolmentViewModel>();


            //    carts.Add(new ChildEnrolmentViewModel { childId = a, isverify = true });
            //    SessionHelper.SetObjectAsJson(HttpContext.Session, "carts", carts);
            //}
            //catch (Exception ex)
            //{

            //}

            // TempData[""]
        }
        public IActionResult indexsave()
        {

            //  var cart = SessionHelper.GetObjectFromJson<List<ChildEnrolmentViewModel>>(HttpContext.Session, "childs1");
            // int a = messIdTemp;

            //   ViewBag.cart = cart;
        
            // string g = ViewBag.temp;
            //// string s = ViewData["temp"].ToString();
            // string b = TempData["alo"].ToString();
            return View();
        }
    }




}
