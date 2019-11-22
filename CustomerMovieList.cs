using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieCruiser
{
    public class CustomerMovieList
    {
       
        public int id { get; set; }
        public CustomerList Customerid { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public long BoxOffice { get; set; }
        public genre Genre { get; set; }

    }
    
}