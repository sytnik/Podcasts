namespace MockShop.Shared.DTO;

public record PersonMapperDTO(int Id, string FirstName, string LastName, int Age,
    string Gender, string Address, List<OrderMapperDTO> Orders);
public record OrderMapperDTO(string Info, string OrderDetailsShippingAddress,
    List<ProductMapperDTO> OrderProducts);

// from OrderProduct
public record ProductMapperDTO(string ProductName, decimal ProductPrice, int Quantity, decimal Total);