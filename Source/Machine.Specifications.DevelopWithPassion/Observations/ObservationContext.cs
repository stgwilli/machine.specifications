using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Machine.Specifications.DevelopWithPassion.DSL.FieldSwitching;

namespace Machine.Specifications.DevelopWithPassion.Observations
{
    public interface ObservationContext
    {
        Dependency the_dependency<Dependency>() where Dependency : class;
        void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value);
        InterfaceType an<InterfaceType>() where InterfaceType : class;
        object an_item_of(Type type);
        void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour);
        void add_pipeline_behaviour(Action context, Action teardown);
    }

    public interface ObservationContext<SUT> : ObservationContext
    {
        void catch_exception(Action because_behaviour);
        void catch_exception<T>(Func<IEnumerable<T>> behaviour);
        ChangeValueInPipeline change(Expression<Func<object>> static_expression);
        void add_sut_pipeline_behaviour(Action<SUT> action);
        Exception exception_thrown_by_the_sut { get; }
        Contract build_sut<Contract, Class>() where Class : Contract;
    }

    public class DefaultObservationContext<SUT> : ObservationContext<SUT>
    {
        MockFactory mock_factory;
        SystemUnderTestDependencyBuilder system_under_test_dependency_builder;
        SystemUnderTestFactory system_under_test_factory;
        Exception exception_thrown;
        TestState<SUT> test_state;
        public Action because_behaviour;

        public DefaultObservationContext(TestState<SUT> test_state_implementation, MockFactory mock_factory,
                                  SystemUnderTestDependencyBuilder system_under_test_dependency_builder,
                                  SystemUnderTestFactory system_under_test_factory)
        {
            this.test_state = test_state_implementation;
            this.mock_factory = mock_factory;
            this.system_under_test_dependency_builder = system_under_test_dependency_builder;
            this.system_under_test_factory = system_under_test_factory;
        }

        public Contract build_sut<Contract, Class>() where Class : Contract
        {
            return system_under_test_factory.create<Contract, Class>();
        }

        public void catch_exception(Action because_behaviour)
        {
            this.because_behaviour = because_behaviour;
        }

        public void catch_exception<T>(Func<IEnumerable<T>> behaviour)
        {
            catch_exception(() => behaviour().Count());
        }

        public Exception exception_thrown_by_the_sut
        {
            get
            {
                return exception_thrown ??
                    (exception_thrown =
                        get_exception_throw_by(because_behaviour));
            }
        }

        public void add_sut_pipeline_behaviour(Action<SUT> action)
        {
            test_state.add_behaviour_against_sut(action);
        }

        Exception get_exception_throw_by(Action action)
        {
            return Catch.Exception(action);
        }

        public object an_item_of(Type type)
        {
            return mock_factory.create_stub(type);
        }

        public InterfaceType an<InterfaceType>() where InterfaceType : class
        {
            return mock_factory.create_stub<InterfaceType>();
        }

        public void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour)
        {
            test_state.add_pipeline_behaviour(pipeline_behaviour);
        }

        public void add_pipeline_behaviour(Action context, Action teardown)
        {
            add_pipeline_behaviour(new PipelineBehaviour(context, teardown));
        }

        public ChangeValueInPipeline change(Expression<Func<object>> static_expression)
        {
            return new ChangeValueInPipeline(add_pipeline_behaviour, static_expression);
        }

        public Dependency the_dependency<Dependency>() where Dependency : class
        {
            return system_under_test_dependency_builder.the_dependency<Dependency>();
        }

        public void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value)
        {
            system_under_test_dependency_builder.provide_a_basic_sut_constructor_argument(value);
        }
    }
}