using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApacheEntities;

namespace ApacheDAL
{
    public class AccountDataAccess
    {
        ApacheDbContext dbCtx;
        public AccountDataAccess()
        {
            dbCtx = new ApacheDbContext();
        }

        public bool AddUser(LoginModel model)
        {
            dbCtx.LoginModels.Add(model);
            dbCtx.SaveChanges();
            return true;
        }

        public LoginModel GetUserById(string id)
        {
            var record = dbCtx.LoginModels.Where(pat => pat.Id == id).SingleOrDefault();
            if (record == null)
            {
                throw new Exception("Record Not Found");
            }
            else
            {
                return record;
            }
        }

        public bool DeleteUser(string id)
        {
            var record = dbCtx.LoginModels.Where(pat => pat.Id == id).SingleOrDefault();
            if (record == null)
            {
                throw new Exception("Record Not Found");
            }
            else
            {
                dbCtx.LoginModels.Remove(record);
                dbCtx.SaveChanges();
                return true;
            }
        }

        public List<LoginModel> GetAllUsers()
        {
            var lstUsers = dbCtx.LoginModels.ToList();
            return lstUsers;
        }
    }
}
