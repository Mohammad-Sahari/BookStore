﻿using System.Runtime.InteropServices.ComTypes;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int TotalPages { get; set; }
        public string Language { get; set; }

    }
}
