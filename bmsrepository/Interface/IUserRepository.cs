using bmsmodel.Common;

namespace bmsrepository.Interface
{
    public interface IUserRepository
    {
        Task<bool> IsExists(UserModel userModel);
        Task Insert(UserModel userModel);
        Task Update(UserModel userModel);
        Task<UserModel?> GetById(long id);
        Task<List<UserModel>> GetAll(UserModel model);
        Task<UserModel?> Login(UserModel userModel);
    }
}
