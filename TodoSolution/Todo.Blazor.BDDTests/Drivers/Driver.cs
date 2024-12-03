using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

namespace Todo.Blazor.BDDTests.Drivers;

public class Driver : IAsyncDisposable
{
    public IConfiguration Configuration { get; private set; } = null!;
    public string BaseUrl { get; private set; } = null!;
    public IPlaywright Playwright { get; private set; } = null!;
    public IBrowser ChromiumBrowser { get; private set; } = null!;
    public IBrowser FirefoxBrowser { get; private set; } = null!;
    public IBrowser WebkitBrowser { get; private set; } = null!;
    public IPage Page { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        BaseUrl = Configuration.GetSection("TestSettings")["WebAppBaseUrl"] ?? string.Empty;

        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        
        ChromiumBrowser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        FirefoxBrowser = await Playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        WebkitBrowser = await Playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
    }

    public async ValueTask DisposeAsync()
    {
        await ChromiumBrowser.CloseAsync();
        await FirefoxBrowser.CloseAsync();
        await WebkitBrowser.CloseAsync();
        Playwright.Dispose();
    }
}