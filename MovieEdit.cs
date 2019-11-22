using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCruiser
{
    public class MovieEdit
    {
        [Key]
        public int MovieId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Title { get; set; }
        [Display(Name ="Gross ($)")]
        public long BoxOffice { get; set; }
        public string Active { get; set; }

        [Required]
        [Display(Name ="Date Of Launch")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfLaunch { get; set; }
        public genre Genre { get; set; }
        public bool HasTeaser { get; set; }

        public CustomerList Customerid { get; set; }


    }

    public enum genre
    {
        ScienceFiction,Superhero,Romance,Comdey,Adventure,Thriller
    }
}