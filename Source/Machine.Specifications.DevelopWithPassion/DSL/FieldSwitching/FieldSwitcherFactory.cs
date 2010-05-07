using System.Reflection;

namespace Machine.Specifications.DevelopWithPassion.DSL.FieldSwitching
{
    public interface FieldSwitcherFactory {
        FieldSwitcher create_to_target(MemberInfo member);
    }

    public class DefaultFieldSwitcherFactory : FieldSwitcherFactory
    {
        MemberTargetRegistry member_target_registry;

        public DefaultFieldSwitcherFactory() :this(new DefaultMemberTargetRegistry()){}

        public DefaultFieldSwitcherFactory(MemberTargetRegistry member_target_registry)
        {
            this.member_target_registry = member_target_registry;
        }

        public FieldSwitcher create_to_target(MemberInfo member)
        {
            return new DefaultFieldSwitcher(member_target_registry.get_member_target_for(member));
        }
    }
        
}