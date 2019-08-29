using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class UsuarioService : Notifiable, IUsuarioService
    {
        private readonly IUsuarioRepository _userRepository;

        public UsuarioService(IUsuarioRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<Usuario> FindAll()
        {
            return _userRepository.FindAll();
        }

        public async Task<Usuario> FindByIdAsync(Guid id)
        {
            return await _userRepository.FindByIdAsync(id);
        }

        public IEnumerable<Usuario> FindAllbyCompany(Guid idCompany)
        {
            return _userRepository.FindAll();
        }
    }
}
