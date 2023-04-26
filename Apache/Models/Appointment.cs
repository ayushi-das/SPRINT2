using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apache.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name ="Email Id")]
        public string PatientId { get; set; }

        [Required, DataType(DataType.Text)]
        [Display(Name ="Name")]
        [StringLength(25, MinimumLength = 3)]
        public string PatientName { get; set; }

        [Required,DataType(DataType.Text)] 
        [Display(Name ="Age")]
        [ValidateAge]
        public int PatientAge { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string DoctorId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name ="Date of Appointment")]
        [ValidateDateOfApp(ErrorMessage = "Please Select a Valid Date")]
        public DateTime DateOfApp { get; set; }

        //[Required]
        public bool Status { get; set; } = false;
    }

    public class ValidateDateOfApp : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var DateOfApp = (DateTime)value;
            if(DateOfApp < DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class ValidateAge : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var Age = (int)value;
            if (Age <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}