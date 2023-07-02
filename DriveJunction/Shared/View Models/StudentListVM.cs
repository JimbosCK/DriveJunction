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
}
