using DriveJunction.EF.Repositories;
using DriveJunction.Models;
using DriveJunction.Shared.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace DriveJunction.Server.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase {
        private readonly IEntityRepo<Student> _studentRepo;

        public StudentController(IEntityRepo<Student> studentRepo) {
            _studentRepo = studentRepo;
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
    }
}
