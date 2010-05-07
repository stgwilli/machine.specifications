using System;
using System.Reflection;

namespace Machine.Specifications.DevelopWithPassion.DSL.FieldSwitching
{
    public interface MemberTargetRegistry
    {
        MemberTarget get_member_target_for(MemberInfo member);
    }
    public class DefaultMemberTargetRegistry : MemberTargetRegistry
    {
        public MemberTarget get_member_target_for(MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Field) return new FieldMemberTarget(member);
            if (member.MemberType == MemberTypes.Property) return new PropertyInfoMemberTarget(member);
            throw new ArgumentException(string.Format("Unable to handle the request member type"));
        }
    }
}