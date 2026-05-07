using bmsmodel.Common;
using bmsrepository.Interface;
using bmsservice.Interface;

namespace bmsservice
{
    internal class UserService(IUserRepository userRepository) : IUserService
    {
        private IUserRepository _userRepository = userRepository;

        public async Task<bool> Insert(UserModel userModel)
        {
            if (await _userRepository.IsExists(userModel))
            {
                return false;
            }

            await _userRepository.Insert(userModel);
            return true;
        }

        public async Task<bool> Update(UserModel userModel)
        {
            if (await _userRepository.IsExists(userModel))
            {
                return false;
            }

            await _userRepository.Update(userModel);
            return true;
        }

        public Task<UserModel?> GetById(long id)
        {
            return _userRepository.GetById(id);
        }

        public Task<List<UserModel>> GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}
