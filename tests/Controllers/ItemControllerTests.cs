using NUnit.Framework;
using FluentAssertions;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

using rest_api_test.Controllers;

namespace rest_api_test.tests.Controllers;

public class ItemControllerTests
{
    private ILogger<ItemController> logger;
    private IMapper mapper;
    private DbContextOptions<ItemContext> options;

    [SetUp]
    public void Setup()
    {
        options = new DbContextOptionsBuilder<ItemContext>()
           .UseInMemoryDatabase(databaseName: "ItemDatabase")
           .Options;

        logger = new Mock<ILogger<ItemController>>().Object;
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingConfiguration)));
        mapper = mapperConfig.CreateMapper();
    }

    [Test]
    public async Task GetReturnsOkObjectResultWithItems()
    {
        using var itemContext = new ItemContext(options);
        itemContext.Add(new Item{Name = "TestItem1"});
        itemContext.Add(new Item{Name = "TestItem2"});
        itemContext.SaveChanges();

        var subject = new ItemController(logger, itemContext, mapper);
    
        var response = await subject.Get();

        response.Should().BeOfType<OkObjectResult>();
        var result = (OkObjectResult)response;
        var expectedItems = new List<ItemResponse> { 
            new ItemResponse { Id = 1, Name = "TestItem1" },
            new ItemResponse {Id = 2, Name = "TestItem2"}
        };
        result.Value.Should().BeEquivalentTo(expectedItems);
    }
}