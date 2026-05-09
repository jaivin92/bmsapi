using bmslib;
using bmslib.Exceptions;
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

        public async Task<UserModel> GetById(long id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<List<UserModel>> GetAll(DataTableRequestModel dataTableRequestModel)
        {
            UserModel _userModel = new();
            if(dataTableRequestModel != null)
            {
                _userModel = dataTableRequestModel.FilterObj.GetModel<UserModel>();
                _userModel.DataTableRequestModel = dataTableRequestModel;
            }
            return await _userRepository.GetAll(_userModel);
        }

        public async Task<UserModel> GetSingle(DataTableRequestModel dataTableRequestModel)
        {
            UserModel _userModel = new();
            if (dataTableRequestModel != null)
            {
                _userModel = dataTableRequestModel.FilterObj.GetModel<UserModel>();
                _userModel.DataTableRequestModel = dataTableRequestModel;
            }
            var result = await _userRepository.GetAll(_userModel);
            return result.FirstOrDefault();
        }

        public async Task<UserModel?> Login(UserModel userModel)
        {
            var user = await _userRepository.Login(userModel);
            if (user == null)
            {
                throw new ValidationException(SystemMessages.User.Invalid());
            }

            return user;
        }
    }
}
