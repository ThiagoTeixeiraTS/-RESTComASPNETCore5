using RestAspNet5.Data.VO;
using RestAspNet5.Model;

namespace RestAspNet5.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user)
    }
}
