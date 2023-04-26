using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApacheEntities
{
    public class Appointment
    {        
        public int Id { get; set; }        
        public Patient Patient { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string PhoneNumber { get; set; }
        public Doctor Doctor { get; set; }
        public string DoctorId { get; set; }
        public DateTime DateOfApp { get; set; }
        public bool Status { get; set; } = false;

    }
}
//all