namespace YamangTao.Core.Common
{
    public interface IIdentifyableEntity<T>
    {
         T EntityId { get; }
    }
}