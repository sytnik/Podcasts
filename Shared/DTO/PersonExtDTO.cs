namespace MockShop.Shared.DTO;

public record PersonExtDTO
    (int Id, string Name, int Age, char Gender, string Address, OrderDTO[] Orders);

public record OrderDTO(string Info, string ShippingAddress, ProductDTO[] Products);

public record ProductDTO(string Name, decimal Price, int Quantity, decimal Total);