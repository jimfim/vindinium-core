using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;
using Module = Autofac.Module;

namespace vindiniumcore.Infrastructure.Modules
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctxt => new MapperConfiguration(cfg =>
            {
                var profiles = ctxt.Resolve<IEnumerable<Profile>>();
                foreach (var profile in profiles) cfg.AddProfile(profile);
            })).SingleInstance();

            builder.Register(ctxt =>
            {
                var scope = ctxt.Resolve<ILifetimeScope>();
                return ctxt.Resolve<MapperConfiguration>().CreateMapper(scope.Resolve);
            });

            RegisterProfiles(builder);
            RegisterTypeConverters(builder);
            RegisterValueResolvers(builder);

            //builder.Register(ctxt =>
            //{
            //    var scope = ctxt.Resolve<ILifetimeScope>();
            //    return ctxt.Resolve<MapperConfiguration>().CreateMapper(scope.Resolve);
            //});

            //register your mapper
            //builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
        }

        private void RegisterProfiles(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<Profile>()
                .As<Profile>();
        }

        private void RegisterTypeConverters(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.GetInterfaces().Any(it => it.Name.Contains("ITypeConverter")))
                .AsSelf();
        }

        private void RegisterValueResolvers(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.GetInterfaces().Any(it => it.Name.Contains("IValueResolver")))
                .AsSelf();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.GetInterfaces().Any(it => it.IsInstanceOfType(typeof(IMemberValueResolver<,,,>))))
                .AsSelf();
        }
    }
}