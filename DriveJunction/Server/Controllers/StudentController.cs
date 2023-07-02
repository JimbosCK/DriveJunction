using DriveJunction.EF.Repositories;
using DriveJunction.Models;
using DriveJunction.Shared.Services;
using DriveJunction.Shared.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace DriveJunction.Server.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase {
        private readonly IEntityRepo<Student> _studentRepo;
        private readonly StudentHandler _studentHandler;

        public StudentController(IEntityRepo<Student> studentRepo, StudentHandler studentHandler) {
            _studentRepo = studentRepo;
            _studentHandler = studentHandler;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentListVM>> Get() {
            IEnumerable<Student> result = await _studentRepo.GetAllAsync();

            return result.Select(student => new StudentListVM() {
                Code = student.Code,
                CreationDate = student.CreationDate,
                FullName = student.FullName,
                PhoneNumber = student.PhoneNumber,

            });
        }


        [HttpGet("{id}")]
        public async Task<StudentEditVM?> Get(Guid id) {
            StudentEditVM student = new();
            try {
                if (id != Guid.Empty) {
                    Student? oldStudent = await _studentRepo.GetByIdAsync(id);
                    if (oldStudent is null) return null;

                    student.FirstName = oldStudent.FirstName;
                    student.LastName = oldStudent.LastName;
                    student.Code = oldStudent.Code;
                    student.PhoneNumber = oldStudent.PhoneNumber;
                }
                return student;
            } catch (Exception) {
                return null;
            }

        }

        [HttpPost]
        public async Task<ActionResult> Post(StudentEditVM student) {
            try {
                if (!_studentHandler.HasValidData(student)) {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity,
                        "The request was well-formed but was unable to be followed due to semantic errors. Check format of 'Name', 'Surname' and 'PhoneNumber'.");
                }
                var newStudent = new Student() {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Code = student.Code,
                    PhoneNumber = student.PhoneNumber,

                };

                await _studentRepo.CreateAsync(newStudent);
                return Ok();
            } catch (Exception e) {
                if (e is Microsoft.Data.SqlClient.SqlException) {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity,
                        "The request was well-formed but was unable to be followed due to semantic errors. 'Customer.CardNumber' might already exist in database.");
                }
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error processing data: " + e.ToString());
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(StudentEditVM student) {
            try {
                var customerToUpdate = await _studentRepo.GetByIdAsync(student.ID);

                if (customerToUpdate is null) return NotFound($"Item with Id = {student.ID} not found");

                if (!_studentHandler.HasValidData(student)) {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity,
                        "The request was well-formed but was unable to be followed due to semantic errors. Check format of 'Name', 'Surname' and 'CardNumber'.");
                }

                await _studentRepo.UpdateAsync(student.ID, new Student() {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Code = student.Code,
                    PhoneNumber = student.PhoneNumber,
                });

                return Ok();
            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error processing data: " + e.ToString());
            }

        }
    }
}