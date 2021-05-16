using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BookProfile: Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}
