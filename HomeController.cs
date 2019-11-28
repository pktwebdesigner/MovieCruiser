using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Data.Objects;

namespace MovieCruiser
{
    public class HomeController : Controller
    {
        private MovieCruisers _context = new MovieCruisers();
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form, CustomerList cus)
        {
            Session["Id"] = form["txtUserId"].ToString();
            var id = int.Parse(Session["Id"].ToString());
            var Pass = form["txtPassword"];
            if (int.Parse(Session["Id"].ToString()) == 1 && Pass.ToLower() == "admin")
            {
                return RedirectToAction("MovieListAdmin",new { name = "admin" });
            }
            var name = _context.customerlist.Where(x => x.Customerid == id).FirstOrDefault();
            var pass = _context.customerlist.Where(x => x.Password == Pass).FirstOrDefault();
            if (name != null && pass != null)
            {
                return RedirectToAction("MovieListCustomer", new { id = int.Parse(Session["Id"].ToString()) });
            }
            else
            {
                ViewBag.Message = "Invalid Login Details";
            }
            return View();
            
        }
        public ActionResult AddMovie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddMovie(MovieEdit Me)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.movieedit.Add(Me);
                    _context.SaveChanges();
                }
                catch { }
             
            }
            return View();

        }

        public ActionResult Edit(int id)
        {
            return View(_context.movieedit.Where(x => x.MovieId == id).FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieEdit movieEdit)
        {
            try
            {
                _context.Entry(movieEdit).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
               
            }
            catch
            {
             
            }
            return RedirectToAction("EditMovieStatus");
        }
         
        public ActionResult EditMovieStatus()
        {
            return View();
        }

        public ActionResult MovieListAdmin()
        {
            var movies = _context.movieedit.ToList();
            return View(movies);
        }

        public ActionResult MovieListCustomer()
        {
         
            
            var moviescustomer = _context.movieedit.ToList();
            return View(moviescustomer);
           
        }

        public ActionResult Register(FormCollection form)
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(CustomerList cslist,FormCollection form)
        {
            try
            {
                int cusid =int.Parse(form["Customerid"]);
                var id = _context.customerlist.Where(x => x.Customerid == cusid);
                if(id==null)
                {
                    _context.customerlist.Add(cslist);
                    _context.SaveChanges();
                    ViewBag.Message = "resgistration sucess";
                }
                else
                {
                    ViewBag.Message = "Customer id already exixts";
                    return RedirectToAction("Register","Home");
                }
             
                
            }
            catch { }
            return View();
        }

        public ActionResult FavoritesListsuccess(int id,CustomerMovieList cmlist)

        {
            try
            {
                var model = _context.movieedit.Where(x => x.MovieId == id).FirstOrDefault();
                var answer = new CustomerMovieList
                {
                    MovieId = model.MovieId,
                    Title = model.Title,
                    Genre = model.Genre,
                    BoxOffice = model.BoxOffice,
                    Customerid=int.Parse(Session["Id"].ToString())
                };
                    _context.CustomerMovieList.Add(answer);
                    _context.SaveChanges();
                    
            }
            
            catch { }
           
            return RedirectToAction("MovieListCustomerNotification"); 
        }

        public ActionResult MovieListCustomerNotification()
        {
            var moviescustomer = _context.movieedit.ToList();
            ViewBag.Message = "Movie added to Favorites Sucessfully";
            return View(moviescustomer);
        }
        public ActionResult CustomerFavList()
        {
            
            var id = (String)Session["Id"];
            int Id = int.Parse(id.ToString());
            var Fav = _context.CustomerMovieList.Where(x => x.Customerid == Id).ToList();
            int i = _context.CustomerMovieList.Where(x => x.Customerid == Id).Count();
            if (i == 0)
            {
                return RedirectToAction("FavListEmpty");
            }
            else
            {
                ViewBag.Message = i;
            }
            return View(Fav);

        }

        public ActionResult FavListEmpty()
        {
            return View();
        }

        public ActionResult DeleteFavMovie(int id)
            
        {
                    var cuss = _context.CustomerMovieList.Where(x => x.MovieId == id).FirstOrDefault();
                    _context.CustomerMovieList.Remove(cuss);
                    _context.SaveChanges();
       
                   return RedirectToAction("FavoritesNotification");
        }

        public ActionResult FavoritesNotification()
        {
                
                var id = int.Parse(Session["Id"].ToString());
                var Fav = _context.CustomerMovieList.Where(x => x.Customerid == id).ToList();
                int i = _context.CustomerMovieList.Where(x => x.Customerid == id).Count();
                if (i == 0)
                {
                    return RedirectToAction("FavListEmpty");
                }
                else
                {
                    ViewBag.Message = i;
                }
                ViewBag.Message1 = "Movie Removed from Favorites List Sucessfully";
                return View(Fav);
                
        }

    }
}