using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPDotNetMVCWithJQuery.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        [Required(ErrorMessage = "FirstName is a required Field")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is a required Field")]
        public string LastName { get; set; } 
        [Required(ErrorMessage = "Age is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "The age must be greater than 0")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Registration Date is a required Field")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate { get; set; }
    }
}