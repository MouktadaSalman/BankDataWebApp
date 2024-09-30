using LocalDBWebAPI.Data;
using LocalDBWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocalDBWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Student> getStudents()
        {
            List<Student> students = DBManager.GetAll();
            return students;
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id) 
        {
            Student student = DBManager.GetById(id);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Student student) 
        {
            if (DBManager.Insert(student))
            {
                return Ok("Successfully inserted");
            }
            return BadRequest("Error in data insertion");
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if(DBManager.Delete(id))
            {
                return Ok("Successfully Deleted");
            }
            return BadRequest("Could not delete");
        }
        [HttpPut]
        public IActionResult Update(Student student)
        {
            if (DBManager.Update(student))
            {
                return Ok("Successfully updated");
            }
            return BadRequest("Could not update");
        }
    }
}
