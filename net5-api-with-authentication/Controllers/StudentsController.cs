using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_5_api_with_authentication.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using net_5_api_with_authentication.Models;
using net_5_api_with_authentication.Repositories;
using net_5_api_with_authentication.Specification;

namespace net_5_api_with_authentication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {

        private readonly SchoolContext _context;
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentRepository studentRepository;

        public StudentsController(ILogger<StudentsController> logger, SchoolContext context, IStudentRepository repository)
        {
            _logger = logger;
            _context = context;
            studentRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            var studentList = await studentRepository.ListAsync(new StudentWithEnrollmentsSpecification());
            return studentList.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost]
        public async Task<ActionResult<ActionResult<Student>>> Post([FromBody] Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = student.ID }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Student student)
        {
            if (id != student.ID)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;
            var findStudent = await _context.Students.FindAsync(id);

            if (findStudent == null)
            {
                return NotFound();
            }
            else
            {
                await _context.SaveChangesAsync();

            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
