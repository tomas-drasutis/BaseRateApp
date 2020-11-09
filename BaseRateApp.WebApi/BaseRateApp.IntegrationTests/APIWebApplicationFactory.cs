using BaseRateApp.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRateApp.IntegrationTests
{
    public class APIWebApplicationFactory : WebApplicationFactory<Startup>
    {
    }
}
