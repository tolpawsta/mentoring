namespace Reflection.Entities.Interfaces
{
    public interface ICustomer
    {
        string Name { get; set; }
        string Buy(string todo);
    }
}