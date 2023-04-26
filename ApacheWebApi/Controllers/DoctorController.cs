using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApacheEntities;
using ApacheDAL;

namespace ApacheWebApi.Controllers
{
    [RoutePrefix("api/Doctor")]
    public class DoctorController : ApiController
    {
        [HttpGet]
        //[Authorize]
        [Route("GetAllDoctors")]
        public List<Doctor> GetDoctors()
        {
            DoctorDataLayer dal = new DoctorDataLayer();
            var lstDoctors = dal.GetAllDoctors();
            return lstDoctors;
        }

        [HttpPost]
        [Authorize]
        [Route("AddDoctor")]
        public void AddDoctor(Doctor doc)
        {
            DoctorDataLayer dal = new DoctorDataLayer();
            dal.AddDoctor(doc);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateDoctor")]
        public void UpdateDoctor(Doctor doc)
        {
            DoctorDataLayer dal = new DoctorDataLayer();
            dal.UpdateDoctorById(doc);
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteDoctor/{id}")]
        public void DoctorDelete(string id)
        {
            DoctorDataLayer dal = new DoctorDataLayer();
            dal.DeleteDoctorById(id);
        }

        [HttpGet]
        [Route("GetDoctorById/{id}")]
        public Doctor GetDoctorById(string id)
        {
            DoctorDataLayer dal = new DoctorDataLayer();
            var record = dal.GetDoctorById(id);
            return record;
        }
    }
}
