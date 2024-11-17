using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.Helpers
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Books, BookModel>()
/*                .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(src => src.LanguageConnection.Name))*/;
        }
    }
}
