using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Repositories;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class UserService : Notifiable, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> FindAll()
        {
            return _userRepository.FindAll();
        }

        public async Task<User> FindByIdAsync(Guid id)
        {
            return await _userRepository.FindByIdAsync(id);
        }

        public IEnumerable<User> FindAllbyCompany(Guid idCompany)
        {
            return _userRepository.FindAll();
        }
    }
}
