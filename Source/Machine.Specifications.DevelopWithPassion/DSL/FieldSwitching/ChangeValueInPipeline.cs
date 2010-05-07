using System;
using System.Linq.Expressions;

namespace Machine.Specifications.DevelopWithPassion.DSL.FieldSwitching
{
    public class ChangeValueInPipeline
    {
        Action<PipelineBehaviour> add_behaviour;
        Expression<Func<object>> member_expression;

        public ChangeValueInPipeline(Action<PipelineBehaviour> add_behaviour, Expression<Func<object>> member_expression)
        {
            this.add_behaviour = add_behaviour;
            this.member_expression = member_expression;
        }

        public void to(object new_value)
        {
            add_behaviour(new FieldReassignmentStartExpression().change(member_expression).to(new_value));
        }
    }
}