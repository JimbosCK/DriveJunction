using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveJunction.Shared.View_Models {
    public class StudentListVM {
        public string Code { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }

        public StudentListVM() {
            Code = String.Empty; FullName = String.Empty; PhoneNumber = String.Empty;
        }
    }

    public class StudentEditVM {
        public Guid ID { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }


        public StudentEditVM() {
            Code = String.Empty; FirstName = String.Empty; LastName = String.Empty; PhoneNumber = String.Empty;
        }
    }
}
