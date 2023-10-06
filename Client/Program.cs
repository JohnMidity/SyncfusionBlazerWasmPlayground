using BlazerApp.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

string LicenseKey = string.Empty;
try
{
    /// Read SyncFusion Licensekey from "syncfusionlicensekey.json"
    HttpClient? http = new HttpClient()
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    };
    builder.Services.AddScoped(sp => http);
    using var response = await http.GetAsync("syncfusionlicensekey.json");
    using var stream = await response.Content.ReadAsStreamAsync();
    builder.Configuration.AddJsonStream(stream);
    LicenseKey = builder.Configuration.GetValue<string>("SyncFusionLicenseKey");
}
finally
{
    Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(LicenseKey);
}



builder.Services.AddSyncfusionBlazor();
await builder.Build().RunAsync();
