using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrashCollectorV2.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        //[Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Address is required.")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        //[Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        //[Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        //[Required(ErrorMessage = "Zip Code is required.")]
        public int ZipCode { get; set; }

        //[Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        //[Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a vaild email.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a vaild email.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        public DayOfWeek? DayOfWeek { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExtraPickUp { get; set; }
        public double? Balance { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }


    }

    public class ViewModel
    {
        private readonly List<Customer> _customers;

        [Display(Name = "Customer Accounts")]
        public int CustomerId { get; set; }

        public IEnumerable<SelectListItem> CustomerDays
        {
            get { return new SelectList(_customers, "Id", "DayOfWeek"); }
        }
    }
}