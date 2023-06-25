namespace MockShop.Shared.DTO;

public record PersonMapperDTO(int Id, string FirstName, string LastName, int Age,
    string Gender, string Address, List<OrderMapperDTO> Orders);
public record OrderMapperDTO(string Info, string OrderDetailsShippingAddress
    // , ProductMapperDTO[] Products
    );

public record ProductMapperDTO(string Name, decimal Price, int Quantity, decimal Total);