using System.Reflection;
using Machine.Specifications.DevelopWithPassion.DSL.FieldSwitching;
using Machine.Specifications.DevelopWithPassion.Extensions;
using Machine.Specifications.DevelopWithPassion.Rhino;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class MemberTargetRegistrySpecs
    {
        public class Item
        {
            public static string static_property { get; set; }
            public static string static_value = "blah";
        }

        public abstract class concern : Observes<MemberTargetRegistry,
                                            DefaultMemberTargetRegistry>
        {
            Establish c = () =>
            {
                property = typeof(Item).GetProperty("static_property");
                field = typeof(Item).GetField("static_value");
            };

            protected static MemberInfo property;
            protected static MemberInfo field;
        }

        [Subject(typeof(DefaultMemberTargetRegistry))]
        public class when_getting_a_member_target_for_a_member_that_represents_a_property : concern
        {
            Because b = () =>
                result = sut.get_member_target_for(property);

            It should_get_a_field_member_target = () =>
                result.ShouldBeAn<PropertyInfoMemberTarget>();

            static MemberTarget result;
        }

        [Subject(typeof(DefaultMemberTargetRegistry))]
        public class when_getting_a_member_target_for_a_member_that_represents_a_field : concern
        {
            Because b = () =>
                result = sut.get_member_target_for(field);

            It should_get_a_field_member_target = () =>
                result.ShouldBeAn<FieldMemberTarget>();

            static MemberTarget result;
        }
    }
}