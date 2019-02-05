using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebSite.DAL.MyEntities;
using MyWebSite.DAL.Repositories.Concrete;

namespace MyWebSite.BLL
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public int AddUser(User item)
        {
            return _userRepository.AddItem(item);
        }

        public int UpdateUser(User item)
        {
            return _userRepository.UpdateITem(item);
        }

        public int DeleteUSer(User item) 
        {
            return _userRepository.DeleteItem(item);
        }

       
    }
}
