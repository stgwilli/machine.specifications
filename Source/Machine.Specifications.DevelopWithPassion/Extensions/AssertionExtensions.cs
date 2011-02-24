using System.Collections.Generic;
using System.Linq;

namespace Machine.Specifications.DevelopWithPassion.Extensions
{
    public static class AssertionExtensions
    {
        public static T ShouldBeAn<T>(this object result)
        {
            result.ShouldBe(typeof(T));
            return (T) result;
        }

        public static void ShouldContainOnlyInOrder<T>(this IEnumerable<T> items, params T[] ordered_items)
        {
            items.ShouldContainOnlyInOrder((IEnumerable<T>) ordered_items);
        }

        public static void ShouldContainOnlyInOrder<T>(this IEnumerable<T> items, IEnumerable<T> ordered_items)
        {
            var source = new List<T>(items);

            if (ordered_items.Where((ordered_element, index) => ! source[index].Equals(ordered_element)).Any())
            {
                throw new SpecificationException(
                    "The set of items should only contain the items in the order {0}\r\nbut it actually contains the items:{1}"
                        .format_using(
                            ordered_items.EachToUsefulString(),
                            items.EachToUsefulString()));
            }
        }
    }
}