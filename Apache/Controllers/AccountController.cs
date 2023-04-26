using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Apache.Models;

namespace Apache.Controllers
{
    [ErrorHandler]
    public class AccountController : Controller
    {
        string baseUrl = "http://localhost:64443/api/Account/";
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            var id = login.Id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetUserById/" + Server.UrlEncode(id));
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsAsync<LoginModel>();
                    data.Wait();

                    var model = data.Result;

                    if(model.Password == login.Password)
                    {
                        FormsAuthentication.SetAuthCookie(login.Id, false);
                        Session.Add("Login", model);
                        //return Redirect(login.ReturnUrl);
                        if (model.Role == "Patient")
                        {

                            return RedirectToAction("GetAppointmentByPatientId", "Appointment", new {id=model.Id});
                        }
                        else if (model.Role == "Staff")
                        {
                            return RedirectToAction("Index", "Staff");
                        }
                        else if(model.Role == "Admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if(model.Role == "Doctor")
                        {
                            return RedirectToAction("GetAppointmentByDocId", "Appointment", new {id=model.Id});
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Invalid Password");
                        return View(login);
                    }
                    //return View(model);
                }
                else
                {
                    ModelState.AddModelError("Id", "Invalid Id");
                    return View(login);
                }
            }           
        }

        public ActionResult SignUpUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUpUser(LoginModel model)
        {
            //string role = "Patient";
            //model.Role= role;
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    var response = client.PostAsJsonAsync<LoginModel>("AddUser",model);
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        FormsAuthentication.SetAuthCookie(model.Id, false);
                        Session.Add("Login", model);
                        if (model.Role == "Patient")
                        {
                            return RedirectToAction("AddPatient", "Patient");
                        }                        
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }                        
                    }
                    else
                    {
                        ModelState.AddModelError("Id", "Server error,Could not Insert");
                        return View(model);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Id", "Some Error Occurred");
                return View(model);
            }
        }

        public ActionResult SignUpStaff()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUpStaff(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    var response = client.PostAsJsonAsync<LoginModel>("AddUser", model);
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ModelState.AddModelError("Id", "Server error,Could not Insert");
                        return View(model);
                    }
                }
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return View();
        }

       
    }
}