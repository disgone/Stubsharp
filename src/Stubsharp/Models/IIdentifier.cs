namespace Stubsharp.Models
{
    public interface IIdentifier<T>
    {
        T Id { get; set; }
    }
}