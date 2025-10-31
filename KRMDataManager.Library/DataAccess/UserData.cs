using KRMDataManager.Library.Internal.DataAccess;
using KRMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRMDataManager.Library.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string  id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new {Id = id};

            var res = sql.LoadData<UserModel, dynamic>("KRMData", p,"dbo.spUserLookup");
            return res;
        }
    }
}
