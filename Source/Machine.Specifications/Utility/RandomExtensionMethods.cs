using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Machine.Specifications.Utility
{
    public static class RandomExtensionMethods
    {
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var t in enumerable) action(t);
        }

        public static void InvokeAll(this IEnumerable<Because> becauseActions)
        {
            InvokeAll(becauseActions.Cast<Delegate>());
        }

        public static void InvokeAll(this IEnumerable<Establish> contextActions)
        {
            InvokeAll(contextActions.Cast<Delegate>());
        }

        public static void InvokeAll(this IEnumerable<Cleanup> contextActions)
        {
            InvokeAll(contextActions.Cast<Delegate>());
        }

        static void InvokeAll(this IEnumerable<Delegate> actions)
        {
            actions.Where(x => x != null).Each(item => item.DynamicInvoke());
        }

        public static bool HasAttribute<TAttribute>(this ICustomAttributeProvider attributeProvider)
        {
            return attributeProvider.GetCustomAttributes(typeof(TAttribute), true).Any();
        }
    }
}