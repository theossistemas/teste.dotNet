using System;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.User;

namespace Api.Service.Services.User
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<UserEntity> _userService;
        public AdminService(IRepository<UserEntity> userService)
        {
            _userService = userService;
        }
        public async Task<bool> IsAdmin(Guid id)
        {
            var user = await _userService.SelectAsync(id);

            if (user.Admin == true)
            {
                return true;
            }
            return false;
        }
    }
}
