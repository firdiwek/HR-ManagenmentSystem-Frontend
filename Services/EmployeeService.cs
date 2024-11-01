using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;
        public EmployeeService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task <List<Employee>?> GetEmployeesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>("Employee");
        }
        
         // Method to get the total count of employees
        public async Task<int> GetEmployeeCountAsync()
        {
            var employees = await GetEmployeesAsync();
            return employees.Count; // Return the total number of employees
        }

       public async Task<List<Employee>> SearchEmployeesAsync(string searchTerm)
{
    // URL-encode the search term to handle special characters
    var encodedSearchTerm = Uri.EscapeDataString(searchTerm);
    
    var response = await _httpClient.GetAsync($"/api/Employee/search?name={encodedSearchTerm}");
    if (response.IsSuccessStatusCode)
    {
        var employees = await response.Content.ReadFromJsonAsync<List<Employee>>();
        return employees ?? new List<Employee>();
    }
    else
    {
        // Optionally log or throw an exception based on the error response
        var errorContent = await response.Content.ReadAsStringAsync();
        throw new Exception($"Error searching employees: {response.StatusCode} - {errorContent}");
    }
}
public async Task<PagedResult<Employee>> GetPagedEmployeesAsync(int page, int pageSize)
{
    var response = await _httpClient.GetAsync($"/api/Employee/paged?page={page}&pageSize={pageSize}");

    if (response.IsSuccessStatusCode)
    {
        var pagedResult = await response.Content.ReadFromJsonAsync<PagedResult<Employee>>();
        return pagedResult ?? new PagedResult<Employee>();
    }
    else
    {
        throw new Exception("Failed to retrieve paged employees.");
    }
}




 public async Task<List<Employee>> GetEmployeesByDepartmentAsync(int departmentId)
{
    var response = await _httpClient.GetAsync($"/api/Employee/department/{departmentId}");
    if (response.IsSuccessStatusCode)
    {
        return await response.Content.ReadFromJsonAsync<List<Employee>>() ?? new List<Employee>();
    }
    else
    {
        throw new Exception("Failed to load employees by department.");
    }
}


        


        public async Task AddEmployeeAsync(Employee employee)
        {
            await _httpClient.PostAsJsonAsync("Employee", employee);
        }
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Employee>($"Employee/{id}");
        
        }
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _httpClient.PutAsJsonAsync($"Employee/{employee.Id}", employee);
        }
        public async Task DeleteEmployeeAsync(int id)
        {
            await _httpClient.DeleteAsync($"Employee/{id}");
        }
    }
}