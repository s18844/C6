using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wyklad5.DTOs.Requests
{
    public class EnrollStudentRequest2
    {
        [Required]
        [MinLength(1)]
        public string Studies { get; set; }


        [Required]
        [RegularExpression("[0-9]*")]
        public int Semester { get; set; }

    }
}
