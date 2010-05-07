using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Machine.Specifications.DevelopWithPassion.DSL.FieldSwitching;

namespace Machine.Specifications.DevelopWithPassion.Observations
{
    public abstract class SupplementarySpecificationContext<Contract, Class,
                                               MockFactoryAdapter>
        where Class : Contract where MockFactoryAdapter : MockFactory, new()
    {
        protected static ObservationController<Contract> spec;
        protected static Contract sut;

        Establish base_establish = () =>
            spec = DefaultObservationController<Contract, Class, MockFactoryAdapter>.New();

        Because pipeline_setup = () =>
            sut = spec.run_setup();

        Cleanup base_cleanup = () =>
            spec.run_tear_down();

        protected static void create_sut_using(SUTFactory<Contract> factory)
        {
            spec.change_sut_factory_to(factory);
        }

        protected static ChangeValueInPipeline change(
            Expression<Func<object>> static_expression)
        {
            return spec.change(static_expression);
        }

        protected static void catch_exception(Action because_behaviour)
        {
            spec.catch_exception(because_behaviour);
        }

        protected static void catch_exception<T>(Func<IEnumerable<T>> behaviour)
        {
            spec.catch_exception(behaviour);
        }

        protected static Exception exception_thrown_by_the_sut
        {
            get { return spec.exception_thrown_by_the_sut; }
        }

        public static object an_item_of(Type type)
        {
            return spec.an_item_of(type);
        }

        protected static InterfaceType an<InterfaceType>()
            where InterfaceType : class
        {
            return spec.an<InterfaceType>();
        }

        protected static void add_pipeline_behaviour(
            PipelineBehaviour pipeline_behaviour)
        {
            spec.add_pipeline_behaviour(pipeline_behaviour);
        }

        protected static void add_pipeline_behaviour(Action context,
                                                     Action teardown)
        {
            spec.add_pipeline_behaviour(context, teardown);
        }
    }
}