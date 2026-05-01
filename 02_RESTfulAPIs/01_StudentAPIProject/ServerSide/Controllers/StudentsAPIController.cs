using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DataSimulation;
using StudentAPI.Models;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace StudentAPI.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Student>> GetAll()
        {
            if (StudentsDataSimulation.StudentsList.Count == 0)
            {
                return NoContent();
            }

            return Ok(StudentsDataSimulation.StudentsList);
        }


        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            return Ok(StudentsDataSimulation.StudentsList.Where(s => s.Grade >= 50));
        }


        [HttpGet("AvgGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<double> GetAverageGrade()
        {
            if (StudentsDataSimulation.StudentsList.Count == 0)
            {
                return Ok(null);
            }

            return Ok(StudentsDataSimulation.StudentsList.Average(s => s.Grade));
        }


        [HttpGet("Get{id}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> GetStudentById(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}.");
            }

            var student = StudentsDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound($"Student not found for ID {id}.");
            }

            return Ok(student);
        }


        [HttpPost("Add", Name = "AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddNewStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name))
            {
                return BadRequest("Student name is required.");
            }

            if (student.Age <= 0)
            {
                return BadRequest("Invalid student age.");
            }

            if (student.Grade <= 0)
            {
                return BadRequest("Invalid student grade.");
            }

            student.Id = StudentsDataSimulation.StudentsList.Max(s => s.Id) + 1;
            StudentsDataSimulation.StudentsList.Add(student);

            return CreatedAtRoute(
                nameof(GetStudentById),
                new { id = student.Id },
                student
                );
        }


        [HttpDelete("Delete{id}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteStudent(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}.");
            }

            var student = StudentsDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            StudentsDataSimulation.StudentsList.Remove(student);
            return Ok($"Student with ID {id} has been deleted successfully.");
        }


        [HttpPut("Update", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateStudent(Student newStudentInfo)
        {
            if (newStudentInfo == null)
            {
                return BadRequest("Student can not be null.");
            }

            var existingStudentInfo = StudentsDataSimulation.StudentsList.FirstOrDefault(s => s.Id == newStudentInfo.Id);

            if (existingStudentInfo == null)
            {
                return BadRequest($"Student with ID {newStudentInfo.Id} not found.");
            }

            if (string.IsNullOrWhiteSpace(newStudentInfo.Name))
            {
                return BadRequest("Student name is required.");
            }

            if (newStudentInfo.Grade < 0)
            {
                return BadRequest("Invalid student grade.");
            }

            if (newStudentInfo.Age < 1)
            {
                return BadRequest("Invalid student age.");
            }

            existingStudentInfo.Name = newStudentInfo.Name;
            existingStudentInfo.Age = newStudentInfo.Age;
            existingStudentInfo.Grade = newStudentInfo.Grade;

            return Ok("Student info updated successfully.");
        }
    }
}
