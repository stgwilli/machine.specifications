using System;

namespace Machine.Specifications.DevelopWithPassion.Observations
{
    public abstract class InstanceObservations<ContractUnderTest, ClassUnderTest,MockingFactory> : SupplementarySpecificationContext<ContractUnderTest,ClassUnderTest,MockingFactory> where ClassUnderTest : ContractUnderTest
        where MockingFactory : MockFactory,new()
    {
        static protected Dependency the_dependency<Dependency>() where Dependency : class
        {
            return spec.the_dependency<Dependency>();
        }

        static protected void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value)
        {
            spec.provide_a_basic_sut_constructor_argument(value);
        }

        static protected void add_pipeline_behaviour_against_sut(Action<ContractUnderTest> action)
        {
            spec.add_pipeline_behaviour_against_sut(action);
        }
    }
}