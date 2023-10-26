namespace NewConsoleDi.Dto;

public sealed class CustomerDto(string firstName, string lastName, OrderDto[] orders)
{
    public string FirstName = firstName;
    public string LastName = lastName;
    public OrderDto[] Orders = orders;
}