namespace Stubsharp.Models.Base
{
    public interface IIdentifier<T>
    {
        T Id { get; set; }
    }
}