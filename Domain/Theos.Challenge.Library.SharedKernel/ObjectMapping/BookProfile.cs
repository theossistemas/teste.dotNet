using AutoMapper;
using Theos.Challenge.Library.SharedKernel.DTO;
using Theos.Challenge.Library.SharedKernel.Entities;

namespace Theos.Challenge.Library.SharedKernel.ObjectMapping
{
    public class BookProfile: Profile
    {
        public BookProfile(){
            CreateMap<Book, BookDTO>();    
        }        
    }
}