using bmsmodel.Common;

namespace bmsrepository.Interface
{
    public interface IUserRepository
    {
        Task Insert(UserModel userModel);
    }
}
