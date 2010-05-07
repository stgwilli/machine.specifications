using System.Reflection;
using Machine.Specifications.DevelopWithPassion.DSL.FieldSwitching;
using Machine.Specifications.DevelopWithPassion.Rhino;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class PropertyInfoTargetSpecs
    {
        public abstract class concern : Observes<MemberTarget,
                                            PropertyInfoMemberTarget>
        {
            Establish c = () =>
            {
                original_value = "original";
                PropertyInfoTargetItem.static_value = original_value;
                member = typeof (PropertyInfoTargetItem).GetProperty("static_value");
                member.ShouldNotBeNull();
                provide_a_basic_sut_constructor_argument(member);
            };

            protected static MemberInfo member;
            protected static string original_value;
        }

        [Subject(typeof (PropertyInfoMemberTarget))]
        public class when_getting_its_value : concern
        {
            Because b = () =>
                result = sut.get_value();


            It should_get_the_value_of_the_field = () =>
                result.ShouldEqual(PropertyInfoTargetItem.static_value);

            static object result;
        }

        [Subject(typeof (FieldMemberTarget))]
        public class when_setting_its_value : concern
        {
            Establish c = () =>
                value_to_change_to = "blasfsfd";

            Because b = () =>
                sut.change_value_to(value_to_change_to);


            It should_change_the_value_of_the_field = () =>
                PropertyInfoTargetItem.static_value.ShouldEqual(value_to_change_to);

            static object result;
            static string value_to_change_to;
        }

        public class PropertyInfoTargetItem
        {
            public static string static_value { get; set; }
        }
    }
}