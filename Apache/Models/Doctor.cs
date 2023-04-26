using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apache.Models
{
    public class Doctor
    {
        [Required,DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string DoctorId { get; set; }
        
        [Required,DataType(DataType.Text)]
        [Display(Name ="Name")]
        [StringLength(25, MinimumLength = 4)]
        public string DoctorName { get; set; }
        
        [Required]
        [Display(Name ="Gender")]
        [ValidateGender(ErrorMessage ="Please Enter 'Male', 'Female' or 'Others'")]
        public Gender DoctorGender { get; set; }
        
        [Required]        
        [DataType(DataType.Date)]
        [ValidateDoBDoctor(ErrorMessage ="Age must be 18 or Greater")]
        public DateTime DateOfBirth { get; set; }
        
        [Required,DataType(DataType.Text),StringLength(20, MinimumLength = 3)]
        public string Specialization { get; set; }
        
        [Required,DataType(DataType.PhoneNumber)]
        [StringLength (11, MinimumLength =7,ErrorMessage ="Please Enter a Valid Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ValidateDoBDoctor : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var Dob = (DateTime)value;
            if((DateTime.Now.Year - Dob.Year) >= 18)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class ValidateGender : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var Gender = (string)value;
            if (Gender=="Male" || Gender=="Female" || Gender=="Others")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}