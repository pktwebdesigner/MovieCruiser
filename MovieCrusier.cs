namespace MovieCruiser
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MovieCruisers : DbContext
    {
        
        public MovieCruisers()
            : base("name=MovieCrusier")
        {
        }

        public DbSet<MovieEdit> movieedit { get; set; }

        public DbSet<CustomerList> customerlist { get; set; }

        public DbSet<CustomerMovieList> CustomerMovieList { get; set; }
    }
}