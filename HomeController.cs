using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Data.Objects;

namespace MovieCruiser.Controllers
{
    public class HomeController : Controller
    {
        private MovieCruisers _context = new MovieCruisers();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form,CustomerList cus)
        {
            var Name = form["txtUserName"];
            var pass = form["txtPassword"];
            if (Name.ToLower() == "admin" && pass.ToLower() == "admin")
            {
                return RedirectToAction("MovieListAdmin");
            }
            else if(Name.ToLower() == "customer" && pass.ToLower() == "customer")
            {
                return RedirectToAction("MovieListCustomer");
            }
            else
            {
                ViewBag.Message = "Incorrect Login Details";
            }
            return View();
            
        }

        public ActionResult AddMovie(MovieEdit Me)
        {
            try
            {
                _context.movieedit.Add(Me);
                _context.SaveChanges();
            }
            catch { }
            return View();

        }

        public ActionResult Edit(int id)
        {
            return View(_context.movieedit.Where(x => x.MovieId == id).FirstOrDefault());
        }

        [HttpPost]
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

        public ActionResult Register()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Register(CustomerList cslist)
        {
            try
            {
                _context.customerlist.Add(cslist);
                _context.SaveChanges();
                ViewBag.Message = "Registration Sucessfull";
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
                    BoxOffice = model.BoxOffice
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
            var Fav = _context.CustomerMovieList.ToList();
            int i=_context.CustomerMovieList.Count();
            if(i==0)
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
            try
            {
                    var cuss = _context.CustomerMovieList.Where(x => x.MovieId == id).FirstOrDefault();
                    _context.CustomerMovieList.Remove(cuss);
                    _context.SaveChanges();
            }
            catch { }
       
            return RedirectToAction("FavoritesNotification");
        }

        public ActionResult FavoritesNotification()
        {
            var Fav = _context.CustomerMovieList.ToList();
            int i = _context.CustomerMovieList.Count();
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