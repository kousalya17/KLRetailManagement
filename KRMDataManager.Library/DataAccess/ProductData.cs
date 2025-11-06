using KRMDataManager.Library.Internal.DataAccess;
using KRMDataManager.Library.Models;
using System.Collections.Generic;

namespace KRMDataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var res = sql.LoadData<ProductModel, dynamic>("KRMData", new {}, "dbo.spProduct_GetAll");
            return res;

        }

    }
}
