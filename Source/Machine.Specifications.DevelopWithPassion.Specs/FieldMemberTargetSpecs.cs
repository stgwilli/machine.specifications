using System;
using System.Reflection;
using Machine.Specifications.DevelopWithPassion.DSL.FieldSwitching;
using Machine.Specifications.DevelopWithPassion.Rhino;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class TheItem
    {
        public static string static_value = "lah";
    }

    public abstract class some_concern : Observes<MemberTarget,
                                             FieldMemberTarget>
    {
        Establish c = () =>
        {
            Func<string> target = () => TheItem.static_value;
            member = typeof(TheItem).GetMember("static_value")[0];
            provide_a_basic_sut_constructor_argument(member);
        };

        protected static MemberInfo member;
    }

    [Subject(typeof(FieldMemberTarget))]
    public class when_getting_Its_value : some_concern
    {
        Establish setup_1 = () => { flag++; };

        Because b = () =>
            result = sut.get_value();

        It should_update = () =>
            flag.ShouldEqual(1);

        It should_get_the_value_of_the_field = () =>
            result.ShouldEqual(TheItem.static_value);

        static object result;
        static int flag;
    }

    [Subject(typeof(FieldMemberTarget))]
    public class when_setting_Its_value : some_concern
    {
        Establish c = () =>
        {
            original_value = TheItem.static_value;
            value_to_change_to = "testing";
            add_pipeline_behaviour(() => {},() => TheItem.static_value = original_value);
        };

        Because b = () =>
            sut.change_value_to(value_to_change_to);

        It should_change_the_value_of_the_field = () =>
            TheItem.static_value.ShouldEqual(value_to_change_to);

        static object result;
        static string value_to_change_to;
        static string original_value;
    }
}