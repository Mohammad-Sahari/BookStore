using System;
using System.Collections;
using System.Collections.Generic;

namespace BookStore.Data
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int TotalPages { get; set; }
        public int LanguageId { get; set; }
        public string CoverImageUrl { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Language Language { get; set; }
        public ICollection<BookGallery> BookGallery { get; set; }
        //public Language LanguageConnection { get; set; }
        //public int LanguageConnectionId { get; set; }
    }

}
