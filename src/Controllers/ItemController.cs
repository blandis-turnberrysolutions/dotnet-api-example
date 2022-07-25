using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;

namespace rest_api_test.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly ILogger<ItemController> _logger;
    private readonly ItemContext _itemContext;
    private readonly IMapper _autoMapper;

    public ItemController(ILogger<ItemController> logger, ItemContext itemContext, IMapper autoMapper)
    {
        _logger = logger;
        _itemContext = itemContext;
        _autoMapper = autoMapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var items = await _itemContext.Items.ProjectTo<ListItemResponse>(_autoMapper.ConfigurationProvider).ToListAsync();

        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> Post(NewItemRequest request) 
    {
        var item = _autoMapper.Map<Item>(request);
        _itemContext.Items.Add(item);

        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id) 
    {
        var item = await _itemContext.Items.ProjectTo<ItemResponse>(_autoMapper.ConfigurationProvider).SingleOrDefaultAsync(i => i.Id == id);
        if(item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put(int id, UpdateItemRequest request) 
    {
        var item = await _itemContext.Items.FirstOrDefaultAsync(i => i.Id == id);
        if(item == null)
        {
            return NotFound();
        }

        _autoMapper.Map(request, item);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _itemContext.Items.FirstOrDefaultAsync(i => i.Id == id);
        if(item == null)
        {
            return NotFound();
        }

        _itemContext.Items.Remove(item);

        return NoContent();
    }
}

public class ItemResponse {
    public int Id { get; set; }
    public string Name { get; set; }
}

public class ListItemResponse {
    public int Id { get; set; }
    public string Name { get; set; }
}

public class NewItemRequest {
    public string Name { get; set; }
}

public class NewItemRequestValidator : AbstractValidator<NewItemRequest>
{
    public NewItemRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(2, 10);
    }
}

public class UpdateItemRequest {
    public string Name { get; set; }
}

public class UpdateItemRequestValidator : AbstractValidator<UpdateItemRequest>
{
    public UpdateItemRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(2, 10);
    }
}