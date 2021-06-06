using net_5_api_with_authentication.Repositories;
using System;
using System.Collections.Generic;

namespace net_5_api_with_authentication.Models
{
    public class Student : BaseEntity, IAggregateRoot
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}
