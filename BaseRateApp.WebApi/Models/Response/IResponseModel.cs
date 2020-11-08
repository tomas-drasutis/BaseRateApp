using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRateApp.Models.Response
{
    public interface IResponseModel<TKey>
    {
        TKey Id { get; set; }
    }
}
