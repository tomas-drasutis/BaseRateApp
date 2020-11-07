using Autofac;
using BaseRateApp.Services;
using BaseRateApp.Services.CustomerService;
using BaseRateApp.Services.CustomerService.Implementations;
using BaseRateApp.Services.Implementations;
using Serilog;

namespace BaseRateApp.WebApi.Middleware
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(x => Log.Logger).SingleInstance();

            builder.RegisterType<AgreementService>().As<IAgreementService>().InstancePerDependency();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerDependency();

        }
    }
}
