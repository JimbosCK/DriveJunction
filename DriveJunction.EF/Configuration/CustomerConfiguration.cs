using DriveJunction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveJunction.EF.Configuration {
    public class StudentConfiguration : IEntityTypeConfiguration<Student> {
        public void Configure(EntityTypeBuilder<Student> builder) {

            builder.ToTable("Students");

            builder.HasKey(student => student.ID);

            builder.Property(student => student.Code).HasMaxLength(10);
            builder.Property(student => student.FirstName).HasMaxLength(50);
            builder.Property(student => student.LastName).HasMaxLength(50);
            builder.Property(student => student.PhoneNumber).HasMaxLength(20);
            builder.Property(student => student.CreationDate);
            builder.Property(student => student.DeletionDate);

            builder.HasIndex(student => student.Code);

            builder.Ignore(student => student.FullName);
        }
    }
}
