using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace n01605783Cumulative3.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }

    
        [Required(ErrorMessage = "Employee Number is required.")]
        public string TeacherEmployeeNumber { get; set; }


        [Required(ErrorMessage = "First Name is required.")]
        public string TeacherFirstName { get; set; }


        [Required(ErrorMessage = "Last Name is required.")]
        public string TeacherLastName { get; set; }


        [Display(Name = "Hire Date")]
        [Required(ErrorMessage = "Hire Date is required")]
        public DateTime TeacherHireDate { get; set; }


        [Display(Name = "Employee Number")]
        [Required(ErrorMessage = "Salary is required.")]
        public string TeacherSalary { get; set; }


        public bool IsValid()
        {
            bool valid = true;

            if (TeacherFirstName == null || TeacherLastName == null || TeacherEmployeeNumber == null)
            {
                //Base validation to check if the fields are entered.
                valid = false;
            }
            else
            {
                //Validation for fields to make sure they meet server constraints
                if (TeacherFirstName.Length < 2 || TeacherFirstName.Length > 255) valid = false;
                if (TeacherLastName.Length < 2 || TeacherLastName.Length > 255) valid = false;
                //C# email regex 
                //https://stackoverflow.com/questions/5342375/regex-email-validation
                //Regex Email = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$");
                //if (!Email.IsMatch(TeacherEmployeeNumber)) valid = false;
            }
            Debug.WriteLine("The model validity is : " + valid);

            return valid;
        }

    }
}