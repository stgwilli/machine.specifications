using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Machine.Specifications.DevelopWithPassion.Extensions;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class TypeExtensionsSpecs
    {
        [Subject(typeof (TypeExtensions))]
        public class when_a_type_is_told_to_find_its_greediest_constructor
        {
            static ConstructorInfo result;

            Because b = () =>
                result = typeof (SomethingWithParameterfulConstructors).greediest_constructor();

            It should_return_the_constructor_that_takes_the_most_arguments = () =>
                result.GetParameters().Count().ShouldEqual(2);
        }

        [Subject(typeof (TypeExtensions))]
        public class when_a_generic_type_is_told_to_return_its_proper_name
        {
            static string result;

            Because b = () =>
                result = typeof (List<int>).proper_name();

            It should_return_a_name_that_has_its_generic_type_arguments_expanded = () =>
                result.ShouldEqual("List`1<System.Int32>");
        }

        public class when_told_to_get_a_list_of_fields_of_certain_type
        {
            static IEnumerable<FieldInfo> result;

            Because b = () =>
            {
                result =
                    typeof (SomethingWithParameterfulConstructors).all_fields_of<SomeThing>(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                                                            BindingFlags.FlattenHierarchy);
            };

            It should_only_return_the_fields_that_match_the_expected_type = () =>
                result.Count().ShouldEqual(2);
        }

        public delegate void SomeThing();

        public class SomethingWithParameterfulConstructors
        {
            public IDbConnection connection { get; set; }

            public IDbCommand command { get; set; }

            public SomethingWithParameterfulConstructors(IDbConnection connection) : this(connection, null)
            {
            }

            public SomethingWithParameterfulConstructors(IDbConnection connection, IDbCommand command)
            {
                this.connection = connection;
                this.command = command;
            }

            SomeThing first_observation = () =>
            {
            };

            SomeThing second_observation = () =>
            {
            };
        }
    }
}