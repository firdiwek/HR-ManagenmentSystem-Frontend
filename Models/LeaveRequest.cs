using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
         public int EmployeeId { get; set; }

        [Required]
        public string LeaveType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; } = "Pending"; // Default to Pending

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; 
        public string ApprovalStatus { get; set; } ="Pending";// Add this property

    }
}