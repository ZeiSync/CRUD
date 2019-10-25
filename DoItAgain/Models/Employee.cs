using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoItAgain.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public Group Group { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public bool Gender { get; set; }

        [Required, StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required, Phone, StringLength(10)]
        public string CMND { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
