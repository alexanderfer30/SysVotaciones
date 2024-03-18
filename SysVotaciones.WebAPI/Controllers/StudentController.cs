﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SysVotaciones.BLL;
using SysVotaciones.EN;

namespace SysVotaciones.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class StudentController : ControllerBase     
    {
        private readonly StudentBLL _studentBLL;

        public StudentController(StudentBLL studentBLL)
        {
            _studentBLL = studentBLL;
        }


        /*
          /Student
        */
        [HttpGet]
        public ActionResult<List<Student>> GetAll()
        {
            List<Student> listStudent = [];
            try
            {
                listStudent = _studentBLL.GetAll();

                return Ok(new { ok = true, data = listStudent });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = listStudent });
            }
        }

        /*
           /Student/{id}
        */
        [HttpGet("{studentCode}")]
        public ActionResult<Student> GetById(string studentCode)
        {
            Student? student = null;
            try
            {
                student = _studentBLL.GeById(studentCode);

                if (student is null) return BadRequest(new { ok = false, data = student });

                return Ok(new { ok = true, data = student });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, data = student });
            }
        }

        /*
           /Student/save
        */
        [HttpPost("save")]
        public IActionResult Save(Student student)
        {
            try
            {
                int rowsAffected = _studentBLL.Save(student);

                if (rowsAffected > 0) return Ok(new { ok = true, message = "Registro guardado" });

                return BadRequest(new { ok = false, message = "Error al guardar" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
            }
        }

        /*
           /Student/login
        */
        [HttpPost("login")]
        public IActionResult Login(User user) 
        {
            try
            {
                var result = _studentBLL.Login(user);

                if (!result.Logged) return BadRequest(new { ok = false, token = "" });

                return Ok(new { ok = true, token = result.Token });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { ok = false, token = "" });
                throw;
            }
        }

        /*
           /Student/delete/{id}
        */
        [HttpDelete("delete/{studentCode}")]
        public IActionResult Delete(string studentCode)
        {
            try
            {
                int rowsAffected = _studentBLL.Delete(studentCode);

                if (rowsAffected != 0) return Ok(new { ok = true, message = "Registro borrado" });

                return BadRequest(new { ok = false, message = "Error al borrar" });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
            }
        }

        /*
           /Student/update
        */
        [HttpPatch("update")]
        public IActionResult Update(Student student)
        {
            try
            {
                int rowsAffected = _studentBLL.Update(student);

                if (rowsAffected > 0) return Ok(new { ok = true, message = "Registro actualizado" });

                return BadRequest(new { ok = false, message = "Error al actualizar" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, message = "Ha ocurrido un error inesperado" });
            }
        }

        [HttpPost("test")]
        public IActionResult Test (string token)
        {
            var claim = Helper.ValidateToken(token);

            if (claim is null) return Unauthorized();
            
            return Ok(claim.Value);
        }
    }
}
