using System.ComponentModel.DataAnnotations;
using bmslib.Resource;
using bmsmodel.Common;
using bmsrepository.Interface;
using bmsservice.Interface;

namespace bmsservice
{
    internal class UserService(IUserRepository userRepository) : IUserService
    {
        private IUserRepository _userRepository = userRepository;

        public async Task Insert(UserModel userModel)
        {
            if (!await _userRepository.IsExists(userModel))
            {
                await _userRepository.Insert(userModel);
            }
            else
            {
                throw new ValidationException(SystemMessages.User.AlreadyExist());
            }
        }

        public async Task Update(UserModel userModel)
        {
            if (!await _userRepository.IsExists(userModel))
            {
                await _userRepository.Update(userModel);
            }
            else
            {
                throw new ValidationException(SystemMessages.User.AlreadyExist());
            }
        }

        public Task<UserModel> GetById(long id)
        {
            return _userRepository.GetById(id);
        }

        public Task<List<UserModel>> GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}
