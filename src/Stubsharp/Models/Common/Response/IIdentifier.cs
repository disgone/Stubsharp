namespace Stubsharp.Models.Common.Response
{
    public interface IIdentifier<T>
    {
        T Id { get; set; }
    }
}