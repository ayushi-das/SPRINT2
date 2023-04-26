using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apache.Models
{
    public class Patient
    {
        [Required,DataType(DataType.EmailAddress)]   
        [Display(Name = "Email Id")]
        public string PatientId { get; set; }

        [Required,DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 3)]
        [Display(Name ="Name")]
        public string PatientName { get; set; }

        [Required]
        [Display(Name ="Gender")]
        [ValidateGender(ErrorMessage = "Please Enter 'Male', 'Female' or 'Others'")]
        public Gender PatientGender { get; set; }
        
        [Required,DataType(DataType.Date)]
        [Display(Name ="Date Of Birth")]
        [ValidateDoB]
        public DateTime DateOfBirth { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
    public enum Gender
    {
        Male,
        Female,
        Others
    }

    public class ValidateDoB : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var Dob = (DateTime)value;
            if(Dob > DateTime.Now.Date)
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