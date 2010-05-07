using System;
using System.Linq.Expressions;
using System.Reflection;
using Machine.Specifications.DevelopWithPassion.Extensions;

namespace Machine.Specifications.DevelopWithPassion.DSL.FieldSwitching
{
    public class FieldReassignmentStartExpression
    {
        FieldSwitcherFactory field_switcher_factory;

        public FieldReassignmentStartExpression() : this(new DefaultFieldSwitcherFactory()) {}

        public FieldReassignmentStartExpression(FieldSwitcherFactory field_switcher_factory)
        {
            this.field_switcher_factory = field_switcher_factory;
        }

        public FieldSwitcher change(Expression<Func<object>> member_expression)
        {
            return field_switcher_factory.create_to_target(get_member_from(member_expression));
        }


        MemberInfo get_member_from(Expression<Func<object>> expression)
        {
            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                var result = expression.Body.downcast_to<UnaryExpression>().Operand;
                return result.downcast_to<MemberExpression>().Member;
            }
            var member_expression = expression.Body.downcast_to<MemberExpression>();
            return member_expression.Member;
        }
    }
}