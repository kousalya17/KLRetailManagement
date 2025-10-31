using KRMDesktopUI.Library.Models;
using System.Threading.Tasks;

namespace KRMDesktopUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo(string Token);
    }
}