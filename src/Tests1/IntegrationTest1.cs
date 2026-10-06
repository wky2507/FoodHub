using Microsoft.AspNetCore.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace FoodHub.UnitTests;

public class CwdTest
{
    private readonly ITestOutputHelper _output;



    // xUnit会自动注入
    public CwdTest(ITestOutputHelper output)
    {
        _output = output;

 
    }

    [Fact]
    public void Test_GetCurrentDirectory()
    {
        var cwd = Directory.GetCurrentDirectory();
        
        var baseDir = AppContext.BaseDirectory;



        //_output.WriteLine($"GetCurrentDirectory={cwd}");
        
        //_output.WriteLine($"AppContext.BaseDirectory={baseDir}");



        Assert.True(true);
    }
}