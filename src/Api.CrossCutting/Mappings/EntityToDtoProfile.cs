using Api.Domain.Dto;
using Api.Domain.Dto.Book;
using Api.Domain.Dto.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {

            //User
            CreateMap<UserEntity, UserDto>()
            .ReverseMap();

            CreateMap<UserEntity, UserDtoCreate>()
            .ReverseMap();

            CreateMap<UserEntity, UserDtoCreateResult>()
            .ReverseMap();

            CreateMap<UserEntity, UserDtoUpdate>()
            .ReverseMap();

            CreateMap<UserEntity, UserDtoUpdateResult>()
            .ReverseMap();

            //Book
            CreateMap<BookEntity, BookDto>()
            .ReverseMap();

            CreateMap<BookEntity, BookDtoCreate>()
            .ReverseMap();

            CreateMap<BookEntity, BookDtoCreateResult>()
            .ReverseMap();

            CreateMap<BookEntity, BookDtoUpdate>()
            .ReverseMap();

            CreateMap<BookEntity, BookDtoUpdateResult>()
            .ReverseMap();
            CreateMap<LogErrorEntity, LogErrorDto>()
            .ReverseMap();
        }
    }
}
