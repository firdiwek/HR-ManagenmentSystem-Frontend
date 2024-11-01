using HR.Management.Client;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;

namespace HR.Management.Client.Services
{
    public class LeaveRequestService
    {
        private readonly HttpClient _httpClient;

        public LeaveRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<LeaveRequest>> GetAllLeaveRequestsAsync()
        {
            var response = await _httpClient.GetAsync("LeaveRequests");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<LeaveRequest>>();
        }

        public async Task<LeaveRequest> GetLeaveRequestByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<LeaveRequest>($"LeaveRequests/{id}");
        }

        public async Task AddLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("LeaveRequests", leaveRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            var response = await _httpClient.PutAsJsonAsync($"LeaveRequests/{leaveRequest.Id}", leaveRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteLeaveRequestAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"LeaveRequests/{id}");
            response.EnsureSuccessStatusCode();
        }

        // Add this method to get leave requests by employee ID
        public async Task<List<LeaveRequest>> GetLeaveRequestsByEmployeeIdAsync(int employeeId)
        {
            var response = await _httpClient.GetAsync($"LeaveRequests/employee/{employeeId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<LeaveRequest>>();
        }


        public async Task ApproveLeaveRequestAsync(int id)
        {
            var leaveRequest = await GetLeaveRequestByIdAsync(id);
            if (leaveRequest != null)
            {
                leaveRequest.ApprovalStatus = "Approved"; // Update status
                await UpdateLeaveRequestAsync(leaveRequest); // Update the request
            }
        }
        public async Task DeclineLeaveRequestAsync(int id)
        {
            var leaveRequest = await GetLeaveRequestByIdAsync(id);
            if (leaveRequest != null)
            {
                leaveRequest.ApprovalStatus = "Declined"; // Update status
                await UpdateLeaveRequestAsync(leaveRequest); // Update the request
            }

    }

}
}