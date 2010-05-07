using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Machine.Specifications.DevelopWithPassion.Extensions;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class TypeCastingExtensionsSpecs
    {
        [Subject(typeof (TypeCastingExtensions))]
        public class when_a_legitimate_downcast_is_made
        {
            It should_not_fail = () =>
                new List<int>().downcast_to<List<int>>();
        }

        [Subject(typeof (TypeCastingExtensions))]
        public class when_determining_if_an_object_is_not_an_instance_of_a_specific_type
        {
            It should_make_determination_based_on_whether_the_object_is_assignable_from_the_specific_type = () =>
            {
                new SqlConnection().is_not_a<IDbCommand>().ShouldBeTrue();
                new SqlConnection().is_not_a<IDbConnection>().ShouldBeFalse();
            };
        }
    }
}