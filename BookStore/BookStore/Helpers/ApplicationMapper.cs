using AutoMapper;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.Helpers
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Books, BookModel>();
        }
    }
}
