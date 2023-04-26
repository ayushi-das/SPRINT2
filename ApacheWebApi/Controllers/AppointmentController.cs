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
    [RoutePrefix("api/Appointment")]
    public class AppointmentController : ApiController
    {
        [HttpGet]
        [Authorize]
        [Route("GetAllAppointments")]
        public List<Appointment> GetAllAppointments()
        {
            AppointmentDataAccess dal = new AppointmentDataAccess();
            var records = dal.GetAllAppointments();
            return records;
        }

        [HttpPost]
        [Authorize]
        [Route("AddAppointment")]
        public void AddAppointment(Appointment app)
        {
            AppointmentDataAccess dal = new AppointmentDataAccess();
            dal.AddAppointment(app);
        }

        [HttpPut]
        [Route("UpdateAppointment")]
        public void UpdateAppointment(Appointment app)
        {
            AppointmentDataAccess dal = new AppointmentDataAccess();
            dal.UpdateAppointmentById(app);
        }

        [HttpDelete]
        [Route("DeleteAppointment/{id}")]
        public void DeleteAppointment(int id)
        {
            AppointmentDataAccess dal = new AppointmentDataAccess();
            dal.DeleteAppointmentById(id);
        }

        [HttpGet]
        [Route("GetAppointmentsByPatientId/{id}")]
        public List<Appointment> GetAppointmentsByPatId(string id)
        {
            AppointmentDataAccess dal = new AppointmentDataAccess();
            var records = dal.GetAppointmentByPatId(id);
            return records;
        }

        [HttpGet]
        [Route("GetAppointmentById/{id}")]
        public Appointment GetAppointmentById(int id)
        {
            AppointmentDataAccess dal = new AppointmentDataAccess();
            var record = dal.GetAppointmentById(id);
            return record;
        }

        [HttpGet]
        [Route("GetAppointmentsByDocId/{id}")]
        public List<Appointment> GetAppointmentByDocId(string id)
        {
            AppointmentDataAccess dal = new AppointmentDataAccess();
            var record = dal.GetAppointmentByDocId(id);
            return record;
        }
    }
}
