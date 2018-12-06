using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollectorV2.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }


        //[Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Zip Code is required.")]
        public int ZipCode { get; set; }

        //[Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        //[Required(ErrorMessage = "Email is required.")]
        //[EmailAddress(ErrorMessage = "Please enter a vaild email.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a vaild email.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}