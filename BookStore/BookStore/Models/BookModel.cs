using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using BookStore.Helpers;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;


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
        [Display(Name ="Choose the book's cover photo")]
        [Required(ErrorMessage ="Book's cover photo must be choosen")]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the book's cover photos")]
        [Required(ErrorMessage = "Book's cover photos must be choosen")]
        public IFormFileCollection GalleryFiles { get; set; }
        public List<GalleryModel> Gallery { get; set; }
        [Display(Name ="Upload your book in pdf format")]
        [Required]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }
    }
  
}
