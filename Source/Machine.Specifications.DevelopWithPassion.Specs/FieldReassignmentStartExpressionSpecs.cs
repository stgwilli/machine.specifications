using System;
using System.Linq.Expressions;
using System.Reflection;
using Machine.Specifications.DevelopWithPassion.DSL.FieldSwitching;
using Machine.Specifications.DevelopWithPassion.Rhino;
using Rhino.Mocks;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class FieldReassignmentStartExpressionSpecs
    {
        public abstract class concern : Observes<FieldReassignmentStartExpression>
        {
            Establish c = () =>
            {
                member_info = typeof (TypeWithAStaticField).GetField("some_value");
                switcher_factory = the_dependency<FieldSwitcherFactory>();
                switcher = an<FieldSwitcher>();
                switcher_factory.Stub(x => x.create_to_target(Arg<MemberInfo>.Is.NotNull)).Return(switcher);
            };

            protected static MemberInfo member_info;
            protected static FieldSwitcherFactory switcher_factory;
            protected static FieldSwitcher switcher;
        }

        [Subject(typeof (FieldReassignmentStartExpression))]
        public class when_provided_the_target_which_it_is_going_to_be_changing : concern
        {
            Establish c = () =>
            {
                var target = typeof (TypeWithAStaticField);
            };

            Because b = () =>
                result = sut.change(() => TypeWithAStaticField.some_value);

            It should_return_a_field_changer_that_can_be_used_to_specify_the_value_for_during_testing = () =>
                result.ShouldEqual(switcher);

            public static Expression<Func<object>> item(Expression<Func<object>> target)
            {
                return target;
            }

            static string new_value;
            static FieldSwitcher result;
        }

        [Subject(typeof (FieldReassignmentStartExpression))]
        public class when_changing_the_target_that_requires_a_boxing_operation_to_be_performed : concern
        {
            Establish c = () =>
            {
                var target = typeof (TypeWithAStaticField);
                boxed_member_info = typeof (TypeWithAStaticField).GetField("some_value_that_will_be_boxed");
            };

            Because b = () =>
                result = sut.change(() => TypeWithAStaticField.some_value_that_will_be_boxed);

            It should_return_a_field_changer_that_can_be_used_to_specify_the_value_for_during_testing = () =>
                result.ShouldEqual(switcher);

            public static Expression<Func<object>> item(Expression<Func<object>> target)
            {
                return target;
            }

            static string new_value;
            static FieldSwitcher result;
            static FieldSwitcherFactory original_factory;
            static FieldInfo boxed_member_info;
        }
    }

    public class TypeWithAStaticField
    {
        public static string some_value = "blah";
        public static int some_value_that_will_be_boxed = 23;
    }

}