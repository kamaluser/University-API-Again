using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
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

            if (_context.Groups.Any(x=>x.No == dto.No && !x.IsDeleted))
            {
                return StatusCode(409, "Group with the same 'No' is already exists.");
            }

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

        [HttpPut("{id}")]
        public ActionResult Update(int id, GroupUpdateDto updateDto)
        {
            var entity = _context.Groups.FirstOrDefault(x => x.Id == id);

            if(entity == null)
            {
                return NotFound();
            }

            if (entity.No != updateDto.No && _context.Groups.Any(x => x.No == updateDto.No && !x.IsDeleted))
            {
                return Conflict();
            }

            entity.No = updateDto.No;
            entity.Limit = updateDto.Limit;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var data = _context.Groups.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (data == null)
            {
                return NotFound();
            }

            data.IsDeleted = true;
            data.ModifiedAt = DateTime.Now;
            _context.SaveChanges();
            return StatusCode(200, "Deleted Successfully.");
        }
    }
} 
