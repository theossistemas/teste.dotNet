using FluentValidation;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.Commom;
using TheosLivraria.Application.UseCases.User.Create.DTOs;
using TheosLivraria.Domain.Interfaces.Data;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.Application.UseCases.User.Create
{
    public class CreateUserUseCase : IUseCase<CreateUserInput, CreateUserOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateUserInput> _validator;

        public CreateUserUseCase(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,

            IValidator<CreateUserInput> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<CreateUserOutput>> ExecuteAsync(CreateUserInput input)
        {
            var validation = await _validator.ValidateAsync(input);

            if (!validation.IsValid)
            {
                var errors = validation.Errors
                    .Select(e => new { Property = e.PropertyName, Error = e.ErrorMessage })
                    .ToList();

                return Result<CreateUserOutput>.Fail("Erro de validação na entidade.", errors);
            }


            var user = await _userRepository.GetByEmailAsync(input.Email);
            if (user != null)
            {
                return Result<CreateUserOutput>.Fail("O e-mail informado já está em uso.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(input.Password);

            var newUser = new UserEntity
            {
                Name = input.Name,
                Email = input.Email,
                Password = passwordHash
            };

            await _userRepository.AddAsync(newUser);
            await _unitOfWork.CommitAsync();

            return Result<CreateUserOutput>.Ok(new CreateUserOutput { Id = newUser.Id });
        }

    }
}
