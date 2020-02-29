using System;

namespace eBank.Core.Abstrations
{
    public interface IRepository<T> : IDisposable where T : IBaseDependency
    {
        ISingleUnit SingleUnit { get; }
    }
}
