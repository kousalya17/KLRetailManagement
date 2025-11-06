using KRMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KRMDesktopUI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}