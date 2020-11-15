using System;

namespace Settlements.Data.Repository
{
    public interface IEntity<out TKey>
       where TKey : IComparable<TKey>
    {
        TKey Id { get; }
    }

    public interface IEntity : IEntity<int> { }
}
