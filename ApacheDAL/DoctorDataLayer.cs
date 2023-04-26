using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApacheEntities;

namespace ApacheDAL
{
    public class DoctorDataLayer
    {
        ApacheDbContext dbCtx;
        public DoctorDataLayer()
        {
            dbCtx = new ApacheDbContext();
        }
        public List<Doctor> GetAllDoctors()
        {
            var lstDoctors = dbCtx.Doctors.ToList();
            return lstDoctors;
        }

        public bool AddDoctor(Doctor doc)
        {
            dbCtx.Doctors.Add(doc) ;
            dbCtx.SaveChanges();
            return true;
        }

        public bool DeleteDoctorById(string id)
        {
            var record = dbCtx.Doctors.Where(pat => pat.DoctorId == id).SingleOrDefault();
            if (record == null)
            {
                throw new Exception("Record Not Found");
            }
            else
            {
                dbCtx.Doctors.Remove(record);
                dbCtx.SaveChanges();
                return true;
            }
        }

        public bool UpdateDoctorById(Doctor doc)
        {
            var record = dbCtx.Doctors.Where(o => o.DoctorId == doc.DoctorId).SingleOrDefault();
            if (record == null)
            {
                throw new Exception("Record Not Found");
            }
            else
            {
                record.DoctorName = doc.DoctorName;
                record.PhoneNumber = doc.PhoneNumber;
                record.Gender = doc.Gender;
                record.DateOfBirth = doc.DateOfBirth;
                record.Specialization = doc.Specialization;

                dbCtx.SaveChanges();
                return true;
            }
        }
        public Doctor GetDoctorById(string id)
        {
            var record = dbCtx.Doctors.Where(pat => pat.DoctorId == id).SingleOrDefault();
            if (record == null)
            {
                throw new Exception("Record Not Found");
            }
            else
            {
                return record;
            }
        }
    }
}
