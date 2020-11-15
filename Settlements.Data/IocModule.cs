using Autofac;
using Settlements.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Settlements.Data
{
    public class IocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SettlementsContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CountryRepository>().As<ICountryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SettlementRepository>().As<ISettlementRepository>().InstancePerLifetimeScope();
        }
    }
}
