using AutoMapper;
using rest_api_test.Controllers;

namespace rest_api_test;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<Item, ItemResponse>();
        CreateMap<UpdateItemRequest, Item>();
        CreateMap<NewItemRequest, Item>();
        CreateMap<Item, ListItemResponse>();
    }
}