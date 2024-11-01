using Microsoft.AspNetCore.Components.Web;
using Services;
using HR.Management.Client.Services;
using Blazorise;

using Blazorise.Charts;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazorise.Icons.Material;
using HR_ManagenmentSystem;




var builder = WebAssemblyHostBuilder.CreateDefault(args); // This line is correct.
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true; // Enables Blazorise to use components immediately
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons()
    .AddMaterialIcons();



// Configure HttpClient for API calls
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("http://localhost:5096/api/") 
});
// Add Blazored.LocalStorage for token storage
builder.Services.AddBlazoredLocalStorage();

// Add custom AuthStateProvider
builder.Services.AddScoped<AuthService>();
builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "Admin"));
});

// Use the custom AuthenticationStateProvider
// builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// Add scoped services for your HR Management System
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<AttendanceService>();
builder.Services.AddScoped<LeaveTypeService>();
builder.Services.AddScoped<LeaveRequestService>();

await builder.Build().RunAsync();

