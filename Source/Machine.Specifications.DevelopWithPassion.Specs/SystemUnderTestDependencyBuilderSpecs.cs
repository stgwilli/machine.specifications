using System.Data;
using Machine.Specifications.DevelopWithPassion.Rhino;
using Rhino.Mocks;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class SystemUnderTestDependencyBuilderSpecs
    {
        public abstract class concern : Observes<SystemUnderTestDependencyBuilder, DefaultSystemUnterTestDependencyBuilder>
        {
            Establish c = () =>
            {
                dependency_bag = MockRepository.GenerateStub<DependencyBag>();
                mock_factory = MockRepository.GenerateStub<MockFactory>();
                create_sut_using(() => new DefaultSystemUnterTestDependencyBuilder(dependency_bag, mock_factory));
            };


            protected static DependencyBag dependency_bag;
            protected static MockFactory mock_factory;
        }

        public class concern_for_requestiong_a_dependency : concern
        {
            Because b = () =>
                result = sut.the_dependency<IDbConnection>();

            protected static IDbConnection result;
        }

        [Subject(typeof(DefaultSystemUnterTestDependencyBuilder))]
        public class when_requesting_a_dependency_and_the_dependencies_have_been_provided : concern_for_requestiong_a_dependency
        {
            Establish c = () =>
            {
                connection = mock_factory.create_stub<IDbConnection>();
                dependency_bag.Stub(x => x.has_no_dependency_for<IDbConnection>()).Return(false);
                dependency_bag.Stub(x => x.get_dependency<IDbConnection>()).Return(connection);
            };

            It should_not_re_add_the_dependency_and_return_the_dependency = () =>
                dependency_bag.never_received(x => x.store_dependency(typeof (IDbConnection), connection));

            It should_return_the_previously_stored_dependency = () =>
                result.ShouldEqual(connection);
        }

        [Subject(typeof (DefaultSystemUnterTestDependencyBuilder))]
        public class when_requesting_a_dependency_and_the_dependencies_have_not_been_provided : concern_for_requestiong_a_dependency
        {
            Establish c = () =>
            {
                dependency_bag.Stub(x => x.has_no_dependency_for<IDbConnection>()).Return(true);
                dependency_bag.Stub(x => x.get_dependency<IDbConnection>()).Return(connection);
            };

            It should_store_the_new_dependency_in_the_dependency_bag = () =>
                dependency_bag.received(x => x.store_dependency(typeof (IDbConnection), connection));


            It should_return_the_dependency = () =>
                result.ShouldEqual(connection);
        }


        protected static IDbConnection connection;
    }
}