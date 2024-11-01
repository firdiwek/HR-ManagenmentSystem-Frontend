using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Models; // Adjust the namespace according to your project structure

namespace Services
{
    public class AttendanceService
    {
        private readonly HttpClient _httpClient;

        public AttendanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all attendance records
        public async Task<List<Attendance>?> GetAttendanceRecordsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Attendance>>("AttendanceRecords");
        }

        // Get a single attendance record by ID
        public async Task<Attendance?> GetAttendanceRecordByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Attendance>($"AttendanceRecords/{id}");
        }
        // Add a new attendance record
        public async Task<int> AddAttendanceRecordAsync(Attendance attendance)
        {
            var response = await _httpClient.PostAsJsonAsync("AttendanceRecords", attendance);
            
            // Check if the response is successful and has a Location header
            response.EnsureSuccessStatusCode(); // Throws an exception if the response is not successful
            
            // Extract the ID from the Location header
            var locationHeader = response.Headers.Location;
            return int.Parse(locationHeader.Segments[^1]); // Assuming the last segment is the ID
        }


        // Update an existing attendance record
        public async Task UpdateAttendanceRecordAsync(Attendance attendance)
        {
            await _httpClient.PutAsJsonAsync($"AttendanceRecords/{attendance.Id}", attendance);
        }

        // Delete an attendance record by ID
        public async Task DeleteAttendanceRecordAsync(int id)
        {
            await _httpClient.DeleteAsync($"AttendanceRecords/{id}");
        }
    }
}
