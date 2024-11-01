using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Attendance
{
    private DateTime? _checkInTime;
    private DateTime? _checkOutTime;

    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    
    public DateTime? CheckInTime
    {
        get => _checkInTime;
        set
        {
            _checkInTime = value;
            CalculateTotalHours();
        }
    }

    public DateTime? CheckOutTime
    {
        get => _checkOutTime;
        set
        {
            _checkOutTime = value;
            CalculateTotalHours();
        }
    }

    public TimeSpan? TotalHours { get; set; }

    private void CalculateTotalHours()
    {
        if (CheckInTime.HasValue && CheckOutTime.HasValue)
        {
            TotalHours = CheckOutTime.Value - CheckInTime.Value;
        }
        else
        {
            TotalHours = null; // Reset if values are not complete
        }
    }
}

}
