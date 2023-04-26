using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apache.Models
{
    public class LoginModel
    {
        [Required,DataType(DataType.EmailAddress)]
        public string Id { get; set; }

        [Required,DataType(DataType.Password)]
        [StringLength(100,MinimumLength =6, ErrorMessage ="Password Length Must Be Atleast 6")]
        public string Password { get; set; }
        
        [ValidateRole(ErrorMessage = "Please input 'Doctor','Staff' or 'Admin'")]
        //[Required]
        public string Role { get; set; }
    }

    public class ValidateRole : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string Role = (string)value;
            if (Role == "Patient" || Role == "Admin" || Role == "Doctor" || Role == "Staff")
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