using AutoMapper;
using MockShop.Shared.DAO;
using MockShop.Shared.DTO;

namespace MockShop.Server.Logic;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateProjection<Person, PersonMapperDTO>();
    }
}