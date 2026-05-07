
using bmsmodel.Common;

namespace bmsservice.Interface
{
    public interface IUserService
    {
        Task<bool> Insert(UserModel userModel);
        Task<bool> Update(UserModel userModel);
        Task<UserModel?> GetById(long id);
        Task<List<UserModel>> GetAll();
    }
}
