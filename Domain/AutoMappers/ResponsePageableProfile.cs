using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ResponsePageableProfile: Profile
    {
        public ResponsePageableProfile()
        {
            CreateMap(typeof(ResponsePageable<>), typeof(ResponsePageable<>));
        }
    }
}
