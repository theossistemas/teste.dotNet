using AutoMapper;
using MMM.Library.Application.ViewModels;
using MMM.Library.Domain.Core.EvetSourcing;
using MMM.Library.Domain.CQRS.Commands;
using MMM.Library.Domain.Models;

namespace MMM.Library.Application.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            // Domain to ViewModel ---------------------------------------------------------
            CreateMap<Book, BookViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<StoredEvent, StoredEventViewModel>().ReverseMap();



            // ViewModel to Domain---------------------------------------------------------
            CreateMap<CategoryViewModel, Category>();

            // CQRS Mapping
            CreateMap<BookViewModel, BookAddCommand>()
                .ConstructUsing(_ => new BookAddCommand(_.CategoryId, _.Title, _.Year, _.Language, _.Location))
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BookViewModel, BookUpdateCommand>()
               .ConstructUsing(_ => new BookUpdateCommand(_.Id, _.CategoryId, _.Title, _.Year, _.Language, _.Location))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BookViewModel, BookDeleteCommand>()
               .ConstructUsing(_ => new BookDeleteCommand(_.Id))
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
