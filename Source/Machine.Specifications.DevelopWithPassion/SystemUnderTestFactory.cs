using System;
using System.Linq;
using Machine.Specifications.DevelopWithPassion.Extensions;

namespace Machine.Specifications.DevelopWithPassion
{
    public interface SystemUnderTestFactory
    {
        Contract create<Contract, Class>() where Class : Contract;
    }

    public class DefaultSystemUnderTestFactory : SystemUnderTestFactory
    {
        SystemUnderTestDependencyBuilder dependency_builder;

        public DefaultSystemUnderTestFactory(SystemUnderTestDependencyBuilder dependency_builder)
        {
            this.dependency_builder = dependency_builder;
        }

        public Contract create<Contract, Class>() where Class : Contract
        {
            var constructor = typeof (Class).greediest_constructor();
            var constructor_parameter_types = constructor.GetParameters().Select(constructor_arg => constructor_arg.ParameterType);
            constructor_parameter_types.each(dependency_builder.register_only_if_missing);
            return (Contract) Activator.CreateInstance(typeof (Class), dependency_builder.all_dependencies(constructor_parameter_types));
        }
    }
}