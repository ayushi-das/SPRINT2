using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApacheEntities;

namespace ApacheDAL
{
    public class ApacheDbContext : DbContext
    {
        public ApacheDbContext() : base("MyConStr")
        {

        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments {get; set;}
        public DbSet<Patient> Patients { get; set; }
        public DbSet<LoginModel> LoginModels { get; set; }
    }
}
