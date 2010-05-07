using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Machine.Specifications.DevelopWithPassion.Rhino;
using Rhino.Mocks;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class SystemUnderTestFactorySpecs
    {
        public abstract class concern : Observes<SystemUnderTestFactory, DefaultSystemUnderTestFactory>
        {
            Establish c = () =>
            {
                connection = MockRepository.GenerateStub<IDbConnection>();
                command = MockRepository.GenerateStub<IDbCommand>();
                builder = MockRepository.GenerateStub<SystemUnderTestDependencyBuilder>();

                builder.Stub(
                    x =>
                    x.all_dependencies(
                        Arg<IEnumerable<Type>>.Matches(
                            args => args.First() == typeof (IDbCommand) && args.Skip(1).Take(1).First() == typeof (IDbConnection)))).Return(
                                new object[] {command, connection});

                create_sut_using(() => new DefaultSystemUnderTestFactory(builder));
            };


            protected static IDbConnection connection;
            protected static IDbCommand command;
            static SystemUnderTestDependencyBuilder builder;
        }

        [Subject(typeof (DefaultSystemUnderTestFactory))]
        public class when_creating_the_system_under_test : concern
        {
            Because b = () =>
            {
                result = sut.create<AClassWithDependencies, AClassWithDependencies>();
            };


            It should_create_an_instance_of_the_system_under_test_using_the_builders_constructor_arg_array = () =>
            {
                result.ShouldNotBeNull();
                result.command.ShouldEqual(command);
                result.connection.ShouldEqual(connection);
            };

            static AClassWithDependencies result;
            static SystemUnderTestDependencyBuilder builder;
        }

        public class AClassWithDependencies
        {
            public IDbCommand command { get; private set; }
            public IDbConnection connection { get; private set; }

            public AClassWithDependencies(IDbCommand command, IDbConnection connection)
            {
                this.command = command;
                this.connection = connection;
            }
        }
    }
}