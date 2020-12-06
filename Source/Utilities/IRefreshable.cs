using System.Threading.Tasks;

namespace JsonFlier.Utilities
{
    public interface IRefreshable
    {
        bool CanRefresh();
        Task Refresh();
    }
}
