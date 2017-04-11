using System.Reflection;
using Autofac;

namespace vindiniumcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildContainer();
        }

        static void BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetEntryAssembly());
            builder.Build();
        }
    }
}