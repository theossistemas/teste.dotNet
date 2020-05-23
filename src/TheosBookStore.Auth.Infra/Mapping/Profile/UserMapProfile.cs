using System.Collections.Generic;
using System.Collections.ObjectModel;

using AutoMapper;
using TheosBookStore.Auth.Domain.Entities;
using TheosBookStore.Auth.Domain.ValueObjects;
using TheosBookStore.Auth.Infra.Models;

namespace TheosBookStore.Auth.Infra.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(user => user.Hash, opt =>
                    opt.MapFrom(entity => entity.Password.Hash))
                .ForMember(model => model.Salt, opt =>
                    opt.MapFrom(entity => entity.Password.Salt));

            CreateMap<UserModel, User>()
                .ForMember(entity => entity.Password, opt =>
                    opt.MapFrom(model =>
                        new Password(model.Hash, model.Salt)
                    )
                );
        }
    }
}
