using AutoMapper;
using StructureMap;
using vindinium.Infrastructure.Mappings;

namespace vindinium.Infrastructure
{
    public class ConfigurationRegistry : Registry
    {
        public ConfigurationRegistry()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<HeroMapping>();
            });
            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            For<IMapper>().Add(() => mapper);
        }
    }
}
