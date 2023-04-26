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
    [RoutePrefix("api/Patient")]
    public class PatientController : ApiController
    {
        [HttpGet]
        //[Authorize]
        [Route("GetAllPatients")]
        public List<Patient> GetPatients()
        {
            PatientDataLayer dal = new PatientDataLayer();
            var lstPatients = dal.GetAllPatients();
            return lstPatients;
        }

        [HttpPost]
        [Authorize]
        [Route("AddPatient")]
        public void AddPatient(Patient patient)
        {
            PatientDataLayer dal = new PatientDataLayer();
            dal.AddPatient(patient);
        }

        [HttpPut]
        [Route("UpdatePatient")]
        public void UpdatePatient(Patient patient)
        {
            PatientDataLayer dal = new PatientDataLayer();
            dal.UpdatePatientById(patient);
        }

        [HttpDelete]
        [Route("DeletePatient/{id}")]
        public void PatientDelete(string id)
        {
            PatientDataLayer dal = new PatientDataLayer();
            dal.DeletePatientById(id);
        }

        [HttpGet]
        [Route("GetPatientById/{id}")]
        public Patient GetPatientById(string id)
        {
            PatientDataLayer dal = new PatientDataLayer();
            var record = dal.GetPatientById(id); 
            return record;
        }
    }
}
