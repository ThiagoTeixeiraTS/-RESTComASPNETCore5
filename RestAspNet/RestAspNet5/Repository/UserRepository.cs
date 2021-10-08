using RestAspNet5.Data.VO;
using RestAspNet5.Model;
using RestAspNet5.Model.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestAspNet5.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlContext _context;
        private byte[] hashedBytes;

        public UserRepository(MySqlContext context)
        {
            _context = context;
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(p => p.Id.Equals(user.Id ))) return null; 
            
            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return user;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return result;

        }



        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(x => (x.UserName == user.UserName) && (x.Password == pass));
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorihtm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            byte[] hashedBytes = algorihtm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);

        }
    }
}
