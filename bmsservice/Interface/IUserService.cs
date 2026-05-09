
using bmsmodel.Common;

namespace bmsservice.Interface
{
    public interface IUserService
    {
        Task Insert(UserModel userModel);
        Task Update(UserModel userModel);
        Task<UserModel> GetById(long id);
        Task<List<UserModel>> GetAll(DataTableRequestModel dataTableRequestModel);
        Task<UserModel> GetSingle(DataTableRequestModel dataTableRequestModel);
        Task<UserModel?> Login(UserModel userModel);
    }
}
