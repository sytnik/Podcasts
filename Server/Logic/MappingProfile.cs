using AutoMapper;
using MockShop.Shared.DAO;
using MockShop.Shared.DTO;

namespace MockShop.Server.Logic;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateProjection<Person, PersonMapperDTO>();
        CreateProjection<Order, OrderMapperDTO>();
        CreateProjection<OrderProduct, ProductMapperDTO>()
            // for records
            .ForCtorParam(nameof(ProductMapperDTO.Total),
                param =>
                    param.MapFrom(orderProduct => orderProduct.Product.Price * orderProduct.Quantity));
    }
}