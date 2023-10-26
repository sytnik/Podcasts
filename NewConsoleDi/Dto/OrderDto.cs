namespace NewConsoleDi.Dto;

public sealed class OrderDto(DateTime date, ProductDto[] products)
{
    public DateTime Date = date;
    public ProductDto[] Products = products;
}