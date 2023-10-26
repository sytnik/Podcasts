namespace NewConsoleDi.Logic;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<Product, ProductDto>();
    }
}