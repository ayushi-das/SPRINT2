using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApacheEntities;

namespace ApacheDAL
{
    public class PatientDataLayer
    {
        ApacheDbContext dbCtx;
        public PatientDataLayer()
        {
            dbCtx = new ApacheDbContext();
        }
        public List<Patient> GetAllPatients()
        {
            try
            {
                var lstPatients = dbCtx.Patients.ToList();
                return lstPatients;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddPatient(Patient patient)
        {
            try
            {
                dbCtx.Patients.Add(patient);
                dbCtx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeletePatientById(string id)
        {
            try
            {
                var record = dbCtx.Patients.Where(pat => pat.PatientId == id).SingleOrDefault();
                if (record == null)
                {
                    throw new Exception("Record Not Found");
                }
                else
                {
                    dbCtx.Patients.Remove(record);
                    dbCtx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdatePatientById(Patient patient)
        {
            try
            {
                var record = dbCtx.Patients.Where(o => o.PatientId == patient.PatientId).SingleOrDefault();
                if (record == null)
                {
                    throw new Exception("Record Not Found");
                }
                else
                {
                    record.PatientName = patient.PatientName;
                    record.PhoneNumber = patient.PhoneNumber;
                    record.Gender = patient.Gender;
                    record.DateOfBirth = patient.DateOfBirth;

                    dbCtx.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Patient GetPatientById(string id)
        {
            try
            {
                var record = dbCtx.Patients.Where(pat => pat.PatientId == id).SingleOrDefault();
                if (record == null)
                {
                    throw new Exception("Record Not Found");
                }
                else
                {
                    return record;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
