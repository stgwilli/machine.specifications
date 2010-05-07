using Machine.Specifications.DevelopWithPassion.Extensions;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class StringExtensionsSpecs
    {
        [Subject(typeof(StringExtensions))]
        public class when_a_string_is_formatted_with_arguments
        {
            static string result;

            Because b = () =>
                result = "this is the {0};".format_using(1);

            It should_return_the_string_formatted_with_the_arguments_specified = () =>
                result.ShouldEqual("this is the 1;");
        }
    }
}