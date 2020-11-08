using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseRateApp.Services.Integrations
{
    public interface IVilibidClient
    {
        Task<decimal> GetBaseRateValue(string BaseRateCode);
    }
}
