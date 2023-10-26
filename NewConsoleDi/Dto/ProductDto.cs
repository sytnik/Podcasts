namespace NewConsoleDi.Dto;

public sealed class ProductDto(string name, decimal price)
{
    public string Name = name;
    public decimal Price = price;
}