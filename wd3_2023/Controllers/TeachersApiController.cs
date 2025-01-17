﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wd3_2023.Data;
using wd3_2023.Models;

namespace wd3_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeachersApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TeachersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeacher()
        {
          if (_context.Teacher == null)
          {
              return NotFound();
          }
            return await _context.Teacher.ToListAsync();
        }

        // GET: api/TeachersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
          if (_context.Teacher == null)
          {
              return NotFound();
          }
            var teacher = await _context.Teacher.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        // PUT: api/TeachersApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.ID)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TeachersApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
          if (_context.Teacher == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Teacher'  is null.");
          }
            _context.Teacher.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.ID }, teacher);
        }

        // DELETE: api/TeachersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            if (_context.Teacher == null)
            {
                return NotFound();
            }
            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return (_context.Teacher?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
