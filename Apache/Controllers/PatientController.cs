using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Apache.Models;

namespace Apache.Controllers
{
    [ErrorHandler]
    public class PatientController : Controller
    {
        string baseUrl = "http://localhost:64443/api/Patient/";
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddPatient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPatient(Patient pat)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    var login = (LoginModel)Session["Login"];

                    var token = TokenManager.GetToken(login);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    client.BaseAddress = new Uri(baseUrl);

                    var response = client.PostAsJsonAsync<Patient>("AddPatient", pat);
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAppointmentByPatientId","Appointment", new {id=pat.PatientId});
                    }
                    else
                    {
                        ModelState.AddModelError("PatientId", "Server error,Could not Insert");
                        return View(pat);
                    }
                }
            }
            else
            {
                return View(pat);
            }
        }
        public ActionResult GetAllPatients()
        {
            var lstPat = new List<Patient>();
            using (var client = new HttpClient())
            {
                //get data from web api
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetAllPatients");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    //read the content from the result
                    var data = result.Content.ReadAsAsync<List<Patient>>();
                    data.Wait();

                    lstPat = data.Result;
                }
            }
            return View(lstPat);
        }
        [HttpGet]
        public ActionResult DeletePatient(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.DeleteAsync("DeletePatient/"+Server.UrlEncode(id));
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllPatients");
                }
                else
                {
                    //ModelState.AddModelError("", "Some Error Occurred");
                    return RedirectToAction("Index","Admin");
                }
            }
        }
        [HttpGet]
        public ActionResult UpdatePatient(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetPatientById/" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsAsync<Patient>();
                    data.Wait();
                    var pat = data.Result;

                    return View(pat);
                }
                else
                {
                    ModelState.AddModelError(id, "Some Error Occurred");
                    return RedirectToAction("Index","Admin");
                }
            }
        }
        [HttpPost]
        public ActionResult UpdatePatient(Patient pat)
        {
            if (ModelState.IsValid)
            {
                //Call API for PUT REQUEST
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var response = client.PutAsJsonAsync<Patient>("UpdatePatient",pat);
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAllPatients");
                    }
                    else
                    {
                        ModelState.AddModelError("PatientId", "Server Error Occurred");
                        return View(pat);
                    }
                }
            }
            else
            {
                return View(pat);
            }
        }
        [HttpGet]
        public ActionResult GetPatientById(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetPatientById/"+Server.UrlEncode(id));
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsAsync<Patient>();
                    data.Wait();

                    var pat = data.Result;
                    return View(pat);
                }
                else
                {
                    return RedirectToAction("Index","Admin");
                }
            }
        }
    }
}