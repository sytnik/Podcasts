using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MockShop.Server.DAO;
using MockShop.Shared.DTO;

namespace MockShop.Server.Logic;

public class ShopRepository
{
    private readonly SampleContext _context;
    private readonly IMapper _mapper;

    public ShopRepository(SampleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PersonDTO>> Persons() =>
        await _context.Persons.Select(person =>
                new PersonDTO(person.Id,
                    $"{person.FirstName} {person.LastName}",
                    person.Age,
                    person.Gender.FirstOrDefault(),
                    person.Address))
            .ToListAsync();
    
    public async Task<List<PersonMapperDTO>> PersonsMapper() =>
        await _mapper.ProjectTo<PersonMapperDTO>(_context.Persons).ToListAsync();
}