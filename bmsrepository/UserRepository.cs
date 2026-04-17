using bmsmodel.Common;
using bmsrepository.Interface;

namespace bmsrepository
{
    internal class UserRepository : IUserRepository
    {
        public Task Insert(UserModel userModel)
        {
            Console.WriteLine(userModel);
            return Task.CompletedTask;
        }
    }
}
