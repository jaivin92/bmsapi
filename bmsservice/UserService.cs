using bmsmodel.Common;
using bmsrepository.Interface;
using bmsservice.Interface;

namespace bmsservice
{
    internal class UserService(IUserRepository userRepository) : IUserService
    {
        private IUserRepository _userRepository = userRepository;

        public Task Insert(UserModel userModel)
        {
            return _userRepository.Insert(userModel);
        }
    }
}
