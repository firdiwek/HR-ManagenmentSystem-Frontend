using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public class DepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get a list of all departments
        public async Task<List<Department>?> GetDepartmentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Department>>("Department");
        }


               // Method to get the total count of employees
        public async Task<int> GetDepartmentCountAsync()
        {
            var departments = await GetDepartmentsAsync();
            return departments.Count; // Return the total number of employees
        }

        // Get a single department by ID
        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Department>($"Department/{id}");
        }

        // Add a new department
        public async Task AddDepartmentAsync(Department department)
        {
            await _httpClient.PostAsJsonAsync("Department", department);
        }

        // Update an existing department
        public async Task UpdateDepartmentAsync(Department department)
        {
            await _httpClient.PutAsJsonAsync($"{department.DepartmentId}", department);
        }

        // Delete a department by ID
        public async Task DeleteDepartmentAsync(int id)
        {
            await _httpClient.DeleteAsync($"Department/{id}");
        }
    }
}
