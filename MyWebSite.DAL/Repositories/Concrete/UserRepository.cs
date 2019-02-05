using MyWebSite.DAL.MyEntities;
using MyWebSite.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyWebSite.DAL.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private MyWebSiteContext _db;

        public UserRepository()
        {
            _db = new MyWebSiteContext();
        }
        public int AddItem(User item)
        {
            _db.User.Add(item);
            var sonuc = _db.SaveChanges();
            return sonuc;
        }

        public int DeleteItem(User item)
        {
            _db.User.Remove(item);
            var sonuc = _db.SaveChanges();
            return sonuc;
        }

        public int UpdateITem(User item)
        {
            User oldUser = _db.User.Where(x => x.UserId == item.UserId).FirstOrDefault();
            oldUser.FirstName = item.FirstName;
            oldUser.LastName = item.LastName;
            oldUser.Gender = item.Gender;
            oldUser.Password = item.Password;
            oldUser.EMail = item.EMail;
            var sonuc = _db.SaveChanges();
            return sonuc;
        }

        public User GetItem(Expression<Func<User, bool>> lambda = null)
        {
            return _db.User.Where(lambda).FirstOrDefault();
        }

        public ICollection<User> GetAllItem(Expression<Func<User, bool>> lambda = null)
        {
            return lambda == null ?
                    _db.User.ToList() : _db.User.Where(lambda).ToList();

        }
    }
}
