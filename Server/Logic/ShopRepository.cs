using Microsoft.EntityFrameworkCore;
using MockShop.Server.DAO;
using MockShop.Shared.DAO;
using MockShop.Shared.DTO;

namespace MockShop.Server.Logic;

public class ShopRepository
{
    private readonly SampleContext _context;

    public ShopRepository(SampleContext context) => _context = context;

    public async Task<List<PersonDTO>> Persons() =>
        await _context.Persons.Select(person =>
                new PersonDTO(person.Id,
                    $"{person.FirstName} {person.LastName}",
                    person.Age,
                    person.Gender.FirstOrDefault(),
                    person.Address))
            .ToListAsync();
}