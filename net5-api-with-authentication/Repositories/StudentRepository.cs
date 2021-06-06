using net_5_api_with_authentication.Data;
using net_5_api_with_authentication.Models;
namespace net_5_api_with_authentication.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolContext dbContext) : base(dbContext)
        {
        }
    }
}
