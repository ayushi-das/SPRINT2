using SampleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Apache.Models
{
    public class TokenManager
    {
        public static string GetToken(LoginModel login)
        {
            using (var client = new HttpClient())
            {
                //Get the token                

                string userName = login.Id;
                string pwd = login.Password;

                string postData = string.Format("grant_type=password&username={0}&password={1}", userName, pwd);
                StringContent content = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");

                var resToken = client.PostAsync("http://localhost:64443/token", content);
                resToken.Wait();

                var resultToken = resToken.Result;
                var dataToken = resultToken.Content.ReadAsAsync<TokenInfo>();
                var tokenInfo = dataToken.Result;
                var tokenStr = tokenInfo.access_token;

                return tokenStr;
            }
        }
    }
}