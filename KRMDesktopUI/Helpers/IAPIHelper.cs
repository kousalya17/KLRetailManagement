using KRMDesktopUI.Models;
using System.Threading.Tasks;

namespace KRMDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}