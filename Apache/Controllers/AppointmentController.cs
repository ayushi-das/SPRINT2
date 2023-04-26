using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Apache.Models;

namespace Apache.Controllers
{
    [ErrorHandler]
    public class AppointmentController : Controller
    {
        string baseUrl = "http://localhost:64443/api/Appointment/";
        // GET: Appointment
        [HttpGet]
        public ActionResult AddAppointment()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult AddAppointment(Appointment app)
        {
            app.Status = false;
            if(ModelState.IsValid)
            {             

                using (var client = new HttpClient())
                {
                    var login = (LoginModel)Session["Login"];

                    var token = TokenManager.GetToken(login);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    client.BaseAddress = new Uri(baseUrl);

                    var response = client.PostAsJsonAsync<Appointment>("AddAppointment", app);
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAppointmentByPatientId", new {id=app.PatientId});
                    }
                    else
                    {
                        ModelState.AddModelError("PatientId", "Server error,Could not Insert");
                        return View(app);
                    }
                }
            }
            else
            {
                return View(app);
            }
        }

        [HttpGet]
        public ActionResult GetAllAppointments()
        {
            var lstApp = new List<Appointment>();
            using (var client = new HttpClient())
            {
                //get data from web api
                var login = (LoginModel)Session["Login"];
                
                var token = TokenManager.GetToken(login);

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetAllAppointments");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    //read the content from the result
                    var data = result.Content.ReadAsAsync<List<Appointment>>();
                    data.Wait();

                    lstApp = data.Result;
                }
            }
            return View(lstApp);
        }

        [HttpGet]
        public ActionResult GetAllAppointmentsForStaff()
        {
            var lstApp = new List<Appointment>();
            using (var client = new HttpClient())
            {
                //get data from web api
                var login = (LoginModel)Session["Login"];

                var token = TokenManager.GetToken(login);

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetAllAppointments");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    //read the content from the result
                    var data = result.Content.ReadAsAsync<List<Appointment>>();
                    data.Wait();

                    lstApp = data.Result;
                }
            }
            return View(lstApp);
        }

        [HttpGet]
        public ActionResult DeleteAppointmentByStaff(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.DeleteAsync("DeleteAppointment/" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllAppointmentsForStaff");
                }
                else
                {
                    //ModelState.AddModelError("", "Some Error Occurred");
                    return RedirectToAction("GetAllAppointmentsForStaff");
                }
            }
        }

        [HttpGet]
        public ActionResult DeleteAppointment(int id,string name)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.DeleteAsync("DeleteAppointment/"+id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAppointmentByPatientId",new {id=name});
                }
                else
                {
                    //ModelState.AddModelError("", "Some Error Occurred");
                    return RedirectToAction("GetAppointmentByPatientId",new {id=name});
                }
            }
        }
        [HttpGet]
        public ActionResult GetAppointmentByPatientId(string id)
        {
            var lstApp = new List<Appointment>();
            using (var client = new HttpClient())
            {
                //get data from web api
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetAppointmentsByPatientId/"+ Server.UrlEncode(id));
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    //read the content from the result
                    var data = result.Content.ReadAsAsync<List<Appointment>>();
                    data.Wait();

                    lstApp = data.Result;
                }
            }
            return View(lstApp);
        }

        [HttpGet]
        public ActionResult GetAppointmentById(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetAppointmentById/"+id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsAsync<Appointment>();
                    data.Wait();

                    var app = data.Result;
                    return View(app);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpGet]
        public ActionResult GetAppointmentByDocId(string id)
        {
            var lstApp = new List<Appointment>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetAppointmentsByDocId/"+Server.UrlEncode(id));
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsAsync<List<Appointment>>();
                    data.Wait();

                    lstApp = data.Result;
                    return View(lstApp);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpGet]
        public ActionResult UpdateAppointment(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetAppointmentById/" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsAsync<Appointment>();
                    data.Wait();
                    var app = data.Result;

                    return View(app);
                }
                else
                {
                    ModelState.AddModelError("id", "Some Error Occurred");
                    return RedirectToAction("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult UpdateAppointment(Appointment app)
        {
            if (ModelState.IsValid)
            {
                //Call API for PUT REQUEST
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var response = client.PutAsJsonAsync<Appointment>("UpdateAppointment", app);
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAppointmentByDocId", new {id=app.DoctorId});
                    }
                    else
                    {
                        ModelState.AddModelError("PatientId", "Server Error Occurred");
                        return View(app);
                    }
                }
            }
            else
            {
                return View(app);
            }
        }

        [HttpGet]
        public ActionResult UpdateAppointmentForPatient(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("GetAppointmentById/" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsAsync<Appointment>();
                    data.Wait();
                    var app = data.Result;

                    return View(app);
                }
                else
                {
                    ModelState.AddModelError("id", "Some Error Occurred");
                    return RedirectToAction("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult UpdateAppointmentForPatient(Appointment app)
        {
            app.Status = false;
            if (ModelState.IsValid)
            {
                //Call API for PUT REQUEST
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var response = client.PutAsJsonAsync<Appointment>("UpdateAppointment", app);
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAppointmentByPatientId/"+app.PatientId);
                    }
                    else
                    {
                        ModelState.AddModelError("PatientId", "Server Error Occurred");
                        return View(app);
                    }
                }
            }
            else
            {
                return View(app);
            }
        }

    }
}