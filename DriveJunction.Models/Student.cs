
namespace DriveJunction.Models {
    public class Student : BaseEntity {
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return LastName + " " + FirstName; } }
        public string PhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }

        public Student() {
            Code = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            PhoneNumber = string.Empty;
        }
    }
}
