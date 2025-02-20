using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityAppAgain.Data;
using UniversityAppAgain.Data.Entities;
using UniversityAppAgain.Dtos.GroupDtos;

namespace UniversityAppAgain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        
        private readonly AppDbContext _context;

        public GroupsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public ActionResult<List<GroupGetDto>> GetAll()
        {
            List<GroupGetDto> dtos = _context.Groups
                .Where(x=>!x.IsDeleted)
                .Select(x => new GroupGetDto
            {
                Id = x.Id,
                No = x.No,
                Limit = x.Limit
            }).ToList();
            return StatusCode(200, dtos);
        }

        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> GetById(int id)
        {
            var data = _context.Groups.FirstOrDefault(x=>x.Id == id && !x.IsDeleted);

            if(data == null)
            {
                return NotFound();
            }

            GroupGetDto dto = new GroupGetDto()
            {
                Id = data.Id,
                No = data.No,
                Limit = data.Limit
            };

            return StatusCode(200, dto);
        }

        [HttpPost("")]
        public ActionResult Create(GroupCreateDto dto)
        {

            if (_context.Groups.Any(x => x.No == dto.No && !x.IsDeleted))
                return StatusCode(409);

            Group group = new Group()
            {
                No = dto.No,
                Limit = dto.Limit,
                CreatedAt = DateTime.Now
            };
            _context.Groups.Add(group);
            _context.SaveChanges();
            return StatusCode(201, new {Id = group.Id });
        }
    }
} 
