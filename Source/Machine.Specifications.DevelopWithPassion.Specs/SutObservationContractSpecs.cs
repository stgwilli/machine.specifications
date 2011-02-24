using System;
using System.Data;
using System.Diagnostics;
using Machine.Specifications.DevelopWithPassion.Extensions;
using Machine.Specifications.DevelopWithPassion.Observations;
using Machine.Specifications.DevelopWithPassion.Rhino;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class SUTObservationContextSpecs
    {
        public class concern : Rhino.Observes
        {
        }

        [Behaviors]
        public class OtherBehaviours
        {
            protected static SomeClass sut;

            It should_update_the_item = () =>
                sut.ShouldNotBeNull();
        }

        [Subject("something")]
        public class when_run_with_other_behaviours : Observes<SomeClass>
        {
            Behaves_like<OtherBehaviours> behaviours;

            It should_run = () => 
                true.ShouldBeTrue();
        }


        [Subject(typeof (SupplementarySpecificationContext<,,>))]
        public class when_a_test_is_run : concern
        {
            Establish c = () => 
                add_pipeline_behaviour(() => number++, () => { });

            It should_be_able_to_modify_the_test_pipeline = () => 
                number.ShouldEqual(1);

            static int number;
        }

        [Subject(typeof (SupplementarySpecificationContext<,,>))]
        public class when_a_test_has_run_its_cleanup : concern
        {
            Establish c = () =>
                add_pipeline_behaviour(() => { }, () => number++);

            It should_run_the_cleanup_pipeline = () =>
            {
            };

            Cleanup clean_up = () =>
            {
                Debug.Assert(number == 1);
            };

            static int number;
        }

        [Subject(typeof (SupplementarySpecificationContext<,,>))]
        public class when_a_test_is_run_that_requires_a_sut : Observes<SomeClass>
        {
            It should_have_automatically_created_the_sut = () => 
                sut.ShouldBeOfType<SomeClass>();
        }


        [Subject(typeof (SupplementarySpecificationContext<,,>))]
        public class when_a_test_is_run_and_the_sut_requires_dependencies : Observes<SomeClassWithDependencies>
        {
            It should_have_automatically_mocked_out_the_dependencies_required_by_the_sut = () =>
            {
                sut.connection.ShouldNotBeNull();
                sut.command.ShouldNotBeNull();
            };
        }

        [Subject(typeof (SupplementarySpecificationContext<,,>))]
        public class when_a_test_changes_the_sut_factory : Observes<SomeClassWithDependencies>
        {
            static IDbConnection connection;
            static IDbCommand command;

            Establish context = () =>
            {
                connection = an<IDbConnection>();
                command = an<IDbCommand>();
                create_sut_using(() => new SomeClassWithDependencies(connection, command));
            };

            It should_use_its_sut_factory_to_create_the_sut = () =>
            {
                sut.connection.ShouldEqual(connection);
                sut.command.ShouldEqual(command);
            };
        }


        [Subject(typeof (SupplementarySpecificationContext<,,>))]
        public class WhenErrorOccursDuringTheTest : Observes<SomeClassWithDependencies>
        {
            static IDbConnection connection;
            static IDbCommand command;

            Establish context = () =>
            {
                connection = an<IDbConnection>();
                command = an<IDbCommand>();
                create_sut_using(() => new SomeClassWithDependencies(connection, command));
            };

            Because b = () => 
                catch_exception(() => throw_something());

            static void throw_something()
            {
                throw new Exception();
            }

            It should_be_able_to_access_the_exception_thrown_by_the_sut = () =>
                exception_thrown_by_the_sut.ShouldBeAn<Exception>();
        }

        public class SomeClassWithDependencies
        {
            public SomeClassWithDependencies(IDbConnection connection, IDbCommand command)
            {
                this.connection = connection;
                this.command = command;
            }

            public IDbConnection connection { get; set; }
            public IDbCommand command { get; set; }
        }
    }

    public class SomeClass
    {
    }
}