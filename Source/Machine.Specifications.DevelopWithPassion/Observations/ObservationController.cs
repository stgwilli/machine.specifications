using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Machine.Specifications.DevelopWithPassion.DSL.FieldSwitching;

namespace Machine.Specifications.DevelopWithPassion.Observations
{
    public interface ObservationController<Contract> : ObservationContext
    {
        Exception exception_thrown_by_the_sut { get; }
        ChangeValueInPipeline change(Expression<Func<object>> expression);
        void catch_exception(Action behaviour);
        void catch_exception<T>(Func<IEnumerable<T>> behaviour);
        void add_pipeline_behaviour_against_sut(Action<Contract> action);
        void change_sut_factory_to(SUTFactory<Contract> factory);
        Contract run_setup();
        void run_tear_down();
    }

    public class DefaultObservationController<Contract, Class, MockFactoryAdapter> :
        ObservationController<Contract> where Class : Contract
                                        where MockFactoryAdapter : MockFactory, new()
    {
        ObservationContext<Contract> obsevation_context;
        TestState<Contract> test_state;


        public static DefaultObservationController<Contract, Class, MockFactoryAdapter> New()
        {
            var controller = new DefaultObservationController<Contract, Class, MockFactoryAdapter>();
            controller.init();
            return controller;
        }

        void init()
        {
            test_state = DefaultTestState<Contract>.New(build_sut);
            obsevation_context = ObservationContextFactory.create_an_observation_context_from(test_state, new MockFactoryAdapter());
        }

        public Contract run_setup()
        {
            return test_state.setup();
        }

        public void run_tear_down()
        {
            test_state.run_teardown_pipeline();
        }

        public void change_sut_factory_to(SUTFactory<Contract> factory)
        {
            test_state.change_sut_factory_to(factory);
        }

        public void add_pipeline_behaviour_against_sut(Action<Contract> action)
        {
            test_state.add_behaviour_against_sut(action);
        }

        public ChangeValueInPipeline change(Expression<Func<object>> expression)
        {
            return obsevation_context.change(expression);
        }

        public void catch_exception(Action behaviour)
        {
            obsevation_context.catch_exception(behaviour);
        }

        public void catch_exception<T>(Func<IEnumerable<T>> behaviour)
        {
            obsevation_context.catch_exception(behaviour);
        }

        public Exception exception_thrown_by_the_sut
        {
            get { return obsevation_context.exception_thrown_by_the_sut; }
        }

        public Dependency the_dependency<Dependency>() where Dependency : class
        {
            return obsevation_context.the_dependency<Dependency>();
        }

        public void provide_a_basic_sut_constructor_argument<ArgumentType>(ArgumentType value)
        {
            obsevation_context.provide_a_basic_sut_constructor_argument(value);
        }

        public InterfaceType an<InterfaceType>() where InterfaceType : class
        {
            return obsevation_context.an<InterfaceType>();
        }

        public object an_item_of(Type type)
        {
            return obsevation_context.an_item_of(type);
        }

        public Contract build_sut()
        {
            return obsevation_context.build_sut<Contract, Class>();
        }

        public void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour)
        {
            obsevation_context.add_pipeline_behaviour(pipeline_behaviour);
        }

        public void add_pipeline_behaviour(Action context, Action teardown)
        {
            obsevation_context.add_pipeline_behaviour(context, teardown);
        }
    }
}