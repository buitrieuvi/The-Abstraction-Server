using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace TheAbstraction.UnitTests
{
    public static class TestOutputHelperExtensions
    {
        public static void WriteJson(this ITestOutputHelper output, object? value)
        {
            var json = JsonConvert.SerializeObject(
            value,
            Formatting.Indented);

            output.WriteLine(json);
        }
    }
}
