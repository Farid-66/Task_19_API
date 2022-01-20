using API.Data;
using API.Models;
using API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStudent()
        {
            List<DtoStudent> model = _context.Students
                                                .Include(g => g.Group)
                                                .Include(p => p.Profession)
                                                .Select(m => new DtoStudent
                                                {
                                                    Id = m.Id,
                                                    Name = m.Name,
                                                    Surname = m.Surname,
                                                    GroupId = m.GroupId,
                                                    Group = new DtoGroup { Id = m.GroupId, Name = m.Group.Name },
                                                    ProfessionId = m.ProfessionId,
                                                    Profession = new DtoProfession { Id = m.ProfessionId, Name = m.Profession.Name }
                                                })
                                                .ToList();

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetModel(int id)
        {
            DtoStudent model = _context.Students
                                                .Include(g => g.Group)
                                                .Include(p => p.Profession)
                                                .Select(m => new DtoStudent
                                                {
                                                    Id = m.Id,
                                                    Name = m.Name,
                                                    Surname = m.Surname,
                                                    GroupId = m.GroupId,
                                                    Group = new DtoGroup { Id = m.GroupId, Name = m.Group.Name },
                                                    ProfessionId = m.ProfessionId,
                                                    Profession = new DtoProfession { Id = m.ProfessionId, Name = m.Profession.Name }
                                                })
                                                .FirstOrDefault(m => m.Id == id);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Create(DtoStudentCreate model)
        {
            Student newStudent = new Student()
            {
                Name = model.Name,
                Surname = model.Surname,
                GroupId = model.GroupId,
                ProfessionId = (int)model.ProfessionId
            };

            _context.Students.Add(newStudent);
            _context.SaveChanges();
            return Ok(newStudent);

        }

        [HttpPatch]
        public IActionResult Update([FromHeader] int? id, [FromBody] DtoStudentCreate dtoStudent)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Student model = _context.Students.Find(id);
            if (model == null)
            {
                return BadRequest();
            }

            model.Name = dtoStudent.Name;
            model.Surname = dtoStudent.Surname;
            model.GroupId = dtoStudent.GroupId;
            model.ProfessionId = dtoStudent.ProfessionId;
            _context.Students.Update(model);
            _context.SaveChanges();

            return Ok(dtoStudent);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Student model = _context.Students.Find(id);
            if (model == null)
            {
                return BadRequest();
            }
            _context.Students.Remove(model);
            _context.SaveChanges();

            return Ok(model);
        }
    }
}
