using System.Net.Http.Json;
using Models;

public class LeaveTypeService
{
    private readonly HttpClient _httpClient;

    public LeaveTypeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LeaveType>> GetLeaveTypesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<LeaveType>>("/api/leavetypes");
    }

  public async Task<int> AddLeaveTypeAsync(LeaveType leaveType)
{
    var response = await _httpClient.PostAsJsonAsync("/api/leavetypes", leaveType);
    
    // Ensure you're checking the response status
    if (response.IsSuccessStatusCode)
    {
        var result = await response.Content.ReadFromJsonAsync<LeaveType>();
        return result.Id; // Assuming Id is returned after creation
    }
    else
    {
        // Handle errors (optional)
        var errorMessage = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error: {errorMessage}");
        return 0; // or throw an exception
    }
}


    public async Task UpdateLeaveTypeAsync(LeaveType leaveType)
    {
        await _httpClient.PutAsJsonAsync($"/api/leavetypes/{leaveType.Id}", leaveType);
    }

    public async Task DeleteLeaveTypeAsync(int id)
    {
        await _httpClient.DeleteAsync($"/api/leavetypes/{id}");
    }
}
