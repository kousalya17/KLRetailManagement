using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRMDesktopUI.Library.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        public decimal GetTaxRate()
        {
            string taxrate = ConfigurationManager.AppSettings["taxRate"];

            bool IsValidTaxRate = decimal.TryParse(taxrate, out decimal res);
            if (IsValidTaxRate == false)
            {
                throw new ConfigurationException("The tax rate is not set properly");
            }
            return res;
        }
    }
}
