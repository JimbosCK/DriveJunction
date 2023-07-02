using DriveJunction.EF.AppContext;
using DriveJunction.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveJunction.EF.Repositories {
    public class StudentRepo : IEntityRepo<Student> {

        private readonly ApplicationContext _appContext;

        public StudentRepo(ApplicationContext appContext) {
            _appContext = appContext;
        }

        public async Task CreateAsync(Student entity) {
            _appContext.Students.Add(entity);

            await _appContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id) {
            var dbStudent = _appContext.Students.SingleOrDefault(dbStudent => dbStudent.ID == id);
            if (dbStudent is null) {
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            }
            _appContext.Students.Remove(dbStudent);
            
            await _appContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync() {
            return await _appContext.Students.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(Guid id) {
            return await _appContext.Students.SingleOrDefaultAsync(student => student.ID == id);
        }

        public async Task UpdateAsync(Guid id, Student entity) {
            var dbStudent = _appContext.Students.SingleOrDefault(dbStudent => dbStudent.ID == id);
            if (dbStudent is null) {
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            }

            dbStudent.Code = entity.Code;
            dbStudent.FirstName = entity.FirstName;
            dbStudent.LastName = entity.LastName;
            dbStudent.PhoneNumber = entity.PhoneNumber;
            dbStudent.CreationDate = entity.CreationDate;
            dbStudent.DeletionDate = entity.DeletionDate;

            await _appContext.SaveChangesAsync();
        }
    }
}
