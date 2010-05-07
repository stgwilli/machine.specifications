using System.Reflection;
using Machine.Specifications.DevelopWithPassion.DSL.FieldSwitching;
using Machine.Specifications.DevelopWithPassion.Rhino;
using Rhino.Mocks;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class FieldSwitcherFactorySpecs
    {
        public abstract class concern : Observes<DefaultFieldSwitcherFactory>
        {
            Establish c = () =>
            {
                member = typeof (Item).GetProperty("static_value");
                registry = the_dependency<MemberTargetRegistry>();
                member_target = an<MemberTarget>();
                member_target.Stub(x => x.get_value()).Return("Blah");

                registry.Stub(x => x.get_member_target_for(member)).Return(member_target);
            };

            protected static PropertyInfo member;
            protected static MemberTargetRegistry registry;
            static MemberTarget member_target;
        }

        [Subject(typeof (DefaultFieldSwitcherFactory))]
        public class when_creating_a_field_switcher : concern
        {
            Because b = () =>
                result = sut.create_to_target(member);

            It should_use_the_member_target_registry_to_create_a_target_to_target_the_member_type = () =>
                result.ShouldBeOfType<DefaultFieldSwitcher>();

            static FieldSwitcher result;
        }
    }

    public class Item
    {
        public static string static_value { get; set; }
    }
}