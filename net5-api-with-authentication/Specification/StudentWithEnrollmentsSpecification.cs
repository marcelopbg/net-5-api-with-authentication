using Ardalis.Specification;
using net_5_api_with_authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_5_api_with_authentication.Specification
{
    public sealed class StudentWithEnrollmentsSpecification : Specification<Student>
    {
        public StudentWithEnrollmentsSpecification(int studentId)
        {
            Query
                .Where(student => student.ID == studentId)
                .Include(student => student.Enrollments)
                .ThenInclude(enrollment => enrollment.Course);
        }
        public StudentWithEnrollmentsSpecification()
        {
            Query
                .Include(student => student.Enrollments)
                .ThenInclude(enrollment => enrollment.Course);
        }
    }
}
