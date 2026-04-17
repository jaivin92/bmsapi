
using bmsmodel.Common;

namespace bmsservice.Interface
{
    public interface IUserService
    {
        Task Insert(UserModel userModel);
    }
}
