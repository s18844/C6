using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wyklad5.DTOs.Requests;
using Wyklad5.DTOs.Responses;

namespace Wyklad5.Services
{
    public interface IStudentDbService
    {
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        EnrollStudentResponse PromoteStudents(int semester, string studies);
        Boolean GetStudent(string index);
    }
}
