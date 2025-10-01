using MediatR;

namespace Library.Application.MediatR.Books.Commands.Delete;

public record BookDeleteCommand(string Id) : IRequest<Unit>;