using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using BookStore.Helpers;
using System.ComponentModel;


namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter The Title")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Please Enter The Author Name")]
        public string Author { get; set; }
        [Required (ErrorMessage ="Please Enter The Book's Description")]
        public string Description { get; set; }
        [Required (ErrorMessage ="Please Enter The Total Pages")]
        public int? TotalPages { get; set; }
        public string Category { get; set; }
        public int LangId { get; set; }
        public string LanguageName { get; set; }
    }
  
}
