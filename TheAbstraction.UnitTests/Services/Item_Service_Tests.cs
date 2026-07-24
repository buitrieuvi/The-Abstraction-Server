using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Commands.InventoryItem.Create;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using Xunit;
using Xunit.Abstractions;

namespace TheAbstraction.UnitTests.Services
{
    public class Item_Service_Tests : IClassFixture<CustomWebApplicationFactory>
    {

        private readonly IServiceProvider _provider;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public Item_Service_Tests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        {
            _provider = factory.Services;
            _client = factory.CreateClient();
            _output = output;
        }

        [Fact]
        public async Task Create_Item()
        {
            using IServiceScope scope = _provider.CreateScope();

            IItemService service = scope.ServiceProvider.GetRequiredService<IItemService>();

            int result = await service.Create("item10");
            _output.WriteLine(result.ToString());
        }

        [Fact]
        public async Task Get_All_Item()
        {
            using IServiceScope scope = _provider.CreateScope();

            IItemService service = scope.ServiceProvider.GetRequiredService<IItemService>();

            List<(string id, string itemName)> result = await service.GetAll();

            List<ItemResponseDTO> list = result
                .Select(x => new ItemResponseDTO
                {
                    Id = x.id,
                    ItemName = x.itemName
                })
                .ToList();

            _output.WriteLine(JsonConvert.SerializeObject(list, Formatting.Indented));
        }
    }
}
