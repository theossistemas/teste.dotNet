using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<Account, AccountDTO>()
                .AfterMap((src, dest, ctx) => {
                    dest.Role = (int)src.Role;
                });
            CreateMap<LoginDTO, Account>();
            CreateMap<AccountInsertDTO, Account>()
                .ForMember(dest => dest.Role, map => map.MapFrom(src => (int)src.Role));
            CreateMap<AccountUpdateDTO, Account>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));
        }
    }
}
