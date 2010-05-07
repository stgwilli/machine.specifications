using System.Collections.Generic;
using System.Linq;
using Machine.Specifications.DevelopWithPassion.Extensions;
using Machine.Specifications.DevelopWithPassion.Rhino;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class AssertionExtensionsSpecs
    {
        public class concern : Observes
        {
        }

        public class when_comparing_two_sets_of_items_for_number_and_order_and_the_sets_are_the_same
        {
            Establish c = () =>
                items = Enumerable.Range(1, 3).ToList();

            Because b = () =>
                items.ShouldContainOnlyInOrder(1, 2, 3);

            It should_not_get_an_exception_when_trying_to_make_the_assertiong = () => { };

            static IList<int> items;
        }

        public class when_comparing_two_sets_of_items_for_number_and_order_and_the_sets_are_not_the_same : Observes
        {
            Establish c = () =>
                items = Enumerable.Range(1, 3).ToList();

            Because b = () =>
                catch_exception(() => items.ShouldContainOnlyInOrder(3, 2, 1));

            It should_not_get_an_exception_when_trying_to_make_the_assertiong = () =>
                exception_thrown_by_the_sut.ShouldBeAn<SpecificationException>();

            static IList<int> items;
        }
    }
}