using System.Threading.Tasks;

namespace eBank.Core.Abstrations
{
    public interface ISingleUnit
    {
        Task<bool> CommitAsync();
    }
}
