using AutoMapper;
using E_Book_Library.DTOs;
using E_Book_Library.Models;

namespace E_Book_Library.Helper
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryDto>();//get
            CreateMap<CategoryDto, Category>();//post
        }
    }
}
