using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public  string Name { get; set; }=string.Empty;
        public  string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}