using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apache.Models;
using System.Net.Http;

namespace Apache.Controllers
{
    [ErrorHandler]
    public class DoctorController : Controller
    {
        string baseUrl = "http://localhost:64443/api/Doctor/";
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddDoctor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDoctor(Doctor doc)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    var login = (LoginModel)Session["Login"];

                    var token = TokenManager.GetToken(login);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    client.BaseAddress = new Uri(baseUrl);

                    var response = client.PostAsJsonAsync<Doctor>("AddDoctor", doc);
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index","Admin");
                    }
                    else
                    {
                        ModelState.AddModelError("DoctorId", "Server error,Could not Insert");
                        return View(doc);
                    }
                }
            }
            else
            {
                return View(doc);
            }
        }
        public ActionResult GetAllDoctors()
        {
            var lstDoc = new List<Doctor>();
            using (var client = new HttpClient())
            {
                //get data from web api
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetAllDoctors");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    //read the content from the result
                    var data = result.Content.ReadAsAsync<List<Doctor>>();
                    data.Wait();

                    lstDoc = data.Result;
                }
            }
            return View(lstDoc);
        }

        public ActionResult GetAllDoctorsForPatient()
        {
            var lstDoc = new List<Doctor>();
            using (var client = new HttpClient())
            {
                //get data from web api
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetAllDoctors");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    //read the content from the result
                    var data = result.Content.ReadAsAsync<List<Doctor>>();
                    data.Wait();

                    lstDoc = data.Result;
                }
            }
            return View(lstDoc);
        }

        public ActionResult GetAllDoctorsByStaff()
        {
            var lstDoc = new List<Doctor>();
            using (var client = new HttpClient())
            {
                //get data from web api
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetAllDoctors");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    //read the content from the result
                    var data = result.Content.ReadAsAsync<List<Doctor>>();
                    data.Wait();

                    lstDoc = data.Result;
                }
            }
            return View(lstDoc);
        }


        [HttpGet]
        public ActionResult DeleteDoctor(string id)
        {
            using (var client = new HttpClient())
            {
                var login = (LoginModel)Session["Login"];

                var token = TokenManager.GetToken(login);

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                client.BaseAddress = new Uri(baseUrl);
                var response = client.DeleteAsync("DeleteDoctor/" + Server.UrlEncode(id));
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllDoctors");
                }
                else
                {
                    //ModelState.AddModelError("", "Some Error Occurred");
                    return RedirectToAction("Index","Admin");
                }
            }
        }
        [HttpGet]
        public ActionResult UpdateDoctor(string id)
        {
            using (var client = new HttpClient())
            {              
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetDoctorById/" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsAsync<Doctor>();
                    data.Wait();
                    var doc = data.Result;

                    return View(doc);
                }
                else
                {
                    ModelState.AddModelError(id, "Some Error Occurred");
                    return RedirectToAction("Index","Admin");
                }
            }
        }
        [HttpPost]
        public ActionResult UpdateDoctor(Doctor doc)
        {
            if (ModelState.IsValid)
            {
                //Call API for PUT REQUEST
                using (var client = new HttpClient())
                {
                    var login = (LoginModel)Session["Login"];

                    var token = TokenManager.GetToken(login);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    client.BaseAddress = new Uri(baseUrl);
                    var response = client.PutAsJsonAsync<Doctor>("UpdateDoctor", doc);
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAllDoctors");
                    }
                    else
                    {
                        ModelState.AddModelError("DoctorId", "Server Error Occurred");
                        return View(doc);
                    }
                }
            }
            else
            {
                return View(doc);
            }
        }
        [HttpGet]
        public ActionResult GetDoctorById(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetDoctorById/" + Server.UrlEncode(id));
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsAsync<Doctor>();
                    data.Wait();

                    var doc = data.Result;
                    return View(doc);
                }
                else
                {
                    return RedirectToAction("Index","Admin");
                }
            }
        }

    }
}