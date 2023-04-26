using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApacheDAL;
using ApacheEntities;

namespace ApacheWebApi.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        [Route("AddUser")]
        public void AddUser(LoginModel model)
        {
            AccountDataAccess dal = new AccountDataAccess();
            dal.AddUser(model);
        }

        [Route("GetUserById/{id}")]
        public LoginModel GetUserById(string id)
        {
            AccountDataAccess dal = new AccountDataAccess();
            var record = dal.GetUserById(id);
            return record;
        }

        [Route("DeleteUser/{id}")]
        public void DeleteUser(string id)
        {
            AccountDataAccess dal = new AccountDataAccess();
            dal.DeleteUser(id);
        }
    }
}
