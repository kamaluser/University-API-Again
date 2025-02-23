using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityAppAgain.Data;
using UniversityAppAgain.Data.Entities;
using UniversityAppAgain.Dtos.StudentDtos;

namespace UniversityAppAgain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public ActionResult<List<StudentGetDto>> GetAll()
        {
            var students = _context.Students.Where(x=>!x.IsDeleted)
                .Select(x=> new StudentGetDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    BirthDate = x.BirthDate,
                    GroupId = x.GroupId
                })
                .ToList();


            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult<StudentGetDto> GetById(int id)
        {
            var student = _context.Students
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if(student == null)
            {
                return NotFound();
            }

            var dto = new StudentGetDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                BirthDate = student.BirthDate,
                GroupId = student.GroupId
            };

            return Ok(dto);
        }

        [HttpPost("")]
        public ActionResult Create(StudentCreateDto createDto)
        {

            if (_context.Students.Any(x=>x.Email == createDto.Email))
            {
                return Conflict();
            }

            var student = new Student
            {
                FullName = createDto.FullName,
                Email = createDto.Email,
                BirthDate = createDto.BirthDate,
                GroupId = createDto.GroupId
            };

            _context.Students.Add(student);
            _context.SaveChanges();

            return StatusCode(201, new { Id = student.Id});
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, StudentUpdateDto updateDto)
        {
            var student = _context.Students.FirstOrDefault(x=>x.Id == id);

            if( student == null )
                return NotFound();

            if (student.Email != updateDto.Email && _context.Students.Any(x=>x.Email == updateDto.Email && !x.IsDeleted))
            {
                return Conflict();
            }


            student.FullName = updateDto.FullName;
            student.Email = updateDto.Email;
            student.BirthDate = updateDto.BirthDate;
            student.GroupId = updateDto.GroupId;
            student.ModifiedAt = DateTime.Now;

            _context.SaveChanges();
            return Ok("Updated Succesfully.");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var data = _context.Students.FirstOrDefault(x=>x.Id == id);

            if (data == null)
                return NotFound();

            data.IsDeleted = true;
            data.ModifiedAt = DateTime.Now;
            _context.SaveChanges();
            return StatusCode(200, "Deleted Successfully.");
        }
    }
}
