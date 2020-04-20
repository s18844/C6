using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wyklad5.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        [Required(ErrorMessage = "Musisz podać ske")]
        [RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }


        [Required(ErrorMessage ="Musisz podać imię")]
        [MaxLength(10)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Musisz podać nazwisko")]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date urodzenia")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Typ studiow")]
        public string Studies { get; set; }
        
    }
}
