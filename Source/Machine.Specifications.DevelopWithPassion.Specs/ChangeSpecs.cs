using System.Security.Principal;
using System.Threading;
using Machine.Specifications.DevelopWithPassion.Rhino;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class ChangeSpecs
    {
        public abstract class concern : Observes
        {
        }

        public class when_a_change_is_made_in_an_establish_block : concern
        {
            Establish c = () =>
            {
                new_value = "other_value";
                change(() => SomeStatic.some_value).to(new_value);
            };

            It should_have_caused_the_change_to_the_field = () =>
                SomeStatic.some_value.ShouldEqual(new_value);

            static string new_value;
        }

        public class when_swapping_the_thread_current_principal : concern
        {
            Establish c = () =>
            {
                principal = an<IPrincipal>();
                change(() => Thread.CurrentPrincipal).to(principal);
            };

            It should_have_the_fake_principal_being_used_as_the_principal = () =>
                Thread.CurrentPrincipal.ShouldEqual(principal);

            static string new_value;
            static IPrincipal principal;
            static object result;
        }

        public class SomeStatic
        {
            public static string some_value = "lah";
        }
    }
}