using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApacheEntities;

namespace ApacheDAL
{
    public class AppointmentDataAccess
    {
        ApacheDbContext dbCtx;

        public AppointmentDataAccess()
        {
            dbCtx = new ApacheDbContext();
        }

        public List<Appointment> GetAllAppointments()
        {
            var lstApp = dbCtx.Appointments.ToList();
            return lstApp;
        }

        public List<Appointment> GetAppointmentByPatId(string id)
        {
            var record = dbCtx.Appointments.Where(pat => pat.PatientId == id).ToList();
            if (record == null)
            {
                throw new Exception("Record Not Found");
            }
            else
            {
                return record;
            }
        }  
        
        public Appointment GetAppointmentById(int id)
        {
            var record = dbCtx.Appointments.Where(p => p.Id == id).SingleOrDefault();
            if(record == null)
            {
                throw new Exception("Record not Found");
            }
            else
            {
                return record;
            }
        }

        public List<Appointment> GetAppointmentByDocId(string id)
        {
            var record = dbCtx.Appointments.Where(p => p.DoctorId == id).ToList();
            if (record == null)
            {
                throw new Exception("Record not Found");
            }
            else
            {
                return record;
            }
        }

        public bool AddAppointment(Appointment app)
        {
            dbCtx.Appointments.Add(app);
            dbCtx.SaveChanges();
            return true;
        }

        public bool DeleteAppointmentById(int id)
        {
            var record = dbCtx.Appointments.Where(pat => pat.Id == id).SingleOrDefault();
            if (record == null)
            {
                throw new Exception("Record Not Found");
            }
            else
            {
                dbCtx.Appointments.Remove(record);
                dbCtx.SaveChanges();
                return true;
            }
        }

        public bool UpdateAppointmentById(Appointment app)
        {
            var record = dbCtx.Appointments.Where(o => o.Id == app.Id).SingleOrDefault();
            if (record == null)
            {
                throw new Exception("Record Not Found");
            }
            else
            {
                record.PatientName = app.PatientName;
                record.PhoneNumber = app.PhoneNumber;
                record.PatientAge = app.PatientAge;
                record.DateOfApp = app.DateOfApp;
                record.Status = app.Status;

                dbCtx.SaveChanges();
                return true;
            }
        }
    }
}
