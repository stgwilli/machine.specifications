using System;
using System.Collections.Generic;
using Machine.Specifications.DevelopWithPassion.Extensions;

namespace Machine.Specifications.DevelopWithPassion
{
    public interface DependencyBag
    {
        void store_dependency(Type type, object instance);
        Dependency get_dependency<Dependency>();
        bool has_no_dependency_for<Dependency>();
        void register_dependency_for_sut(Type dependency_type, object instance);
        bool has_no_dependency_for(Type dependency_type);
        object get_the_provided_dependency_assignable_from(Type constructor_parament_type);
        void empty_dependencies();
    }

    public interface TestState<SUT> : DependencyBag
    {
        void run_teardown_pipeline();
        void clear_test_pipeline();
        void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour);
        void add_behaviour_against_sut(Action<SUT> action);
        void change_sut_factory_to(SUTFactory<SUT> factory);
        SUT setup();
    }

    public class DefaultTestState<SUT> : TestState<SUT>
    {
        IList<PipelineBehaviour> pipeline_behaviours;
        IList<Action<SUT>> sut_context_behaviours;
        IDictionary<Type, object> dependencies;
        SUTFactory<SUT> factory;
        public SUT sut;


        public static DefaultTestState<SUT> New(SUTFactory<SUT> factory)
        {
            return new DefaultTestState<SUT>(factory, new List<PipelineBehaviour>());
        }

        public DefaultTestState(SUTFactory<SUT> factory, IList<PipelineBehaviour> behaviours)
        {
            this.factory = factory;

            pipeline_behaviours = pipeline_behaviours = behaviours;
            sut_context_behaviours = new List<Action<SUT>>();
            dependencies = new Dictionary<Type, object>();
        }

        public void change_sut_factory_to(SUTFactory<SUT> factory)
        {
            this.factory = factory;
        }

        public SUT setup()
        {
            run_startup_pipeline();
            run_startup_pipeline_that_requires_sut();
            build_sut();
            return sut;
        }

        public void build_sut()
        {
            sut = factory();
        }

        public SUT create_sut()
        {
            return factory();
        }

        void run_startup_pipeline_that_requires_sut()
        {
            sut_context_behaviours.each(action => action(sut));
        }

        void run_startup_pipeline()
        {
            pipeline_behaviours.each(item => item.start());
        }

        public void run_teardown_pipeline()
        {
            pipeline_behaviours.each(item => item.finish());
        }

        public void clear_test_pipeline()
        {
            pipeline_behaviours.Clear();
        }

        public void store_dependency(Type type, object instance)
        {
            dependencies.Add(type, instance);
        }

        public Dependency get_dependency<Dependency>()
        {
            return (Dependency) dependencies[typeof (Dependency)];
        }

        public bool has_no_dependency_for<Interface>()
        {
            return has_no_dependency_for(typeof (Interface));
        }

        public bool has_no_dependency_for(Type dependency_type)
        {
            return ! dependencies.ContainsKey(dependency_type);
        }

        public void register_dependency_for_sut(Type dependency_type, object instance)
        {
            dependencies[dependency_type] = instance;
        }

        public object get_the_provided_dependency_assignable_from(Type constructor_parament_type)
        {
            return dependencies[constructor_parament_type];
        }

        public void empty_dependencies()
        {
            dependencies.Clear();
        }

        public void add_pipeline_behaviour(PipelineBehaviour pipeline_behaviour)
        {
            pipeline_behaviours.Add(pipeline_behaviour);
        }

        public void add_behaviour_against_sut(Action<SUT> action)
        {
            sut_context_behaviours.Add(action);
        }
    }
}