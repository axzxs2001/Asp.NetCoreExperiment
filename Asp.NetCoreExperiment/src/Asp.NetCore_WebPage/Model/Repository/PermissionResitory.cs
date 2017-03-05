using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model.Repository
{
    public class PermissionResitory : IPermissionResitory
    {
        ExperimentPageContext _dbContext;
        public PermissionResitory(ExperimentPageContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool AddUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                var result = _dbContext.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public User GetUser(int id)
        {
            try
            {
                return _dbContext.Users.SingleOrDefault(w => w.ID == id);
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                return _dbContext.Users.ToList();
            }
            catch (Exception exc)
            {
                return new List<User>();
            }
        }

        public bool ModifyUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                var result = _dbContext.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                var user = _dbContext.Users.SingleOrDefault(w => w.ID == id);
                _dbContext.Users.Remove(user);
                var result = _dbContext.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }
    }
}
