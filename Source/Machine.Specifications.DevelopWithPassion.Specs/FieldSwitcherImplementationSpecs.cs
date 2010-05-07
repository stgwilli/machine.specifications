using Machine.Specifications.DevelopWithPassion.DSL.FieldSwitching;
using Machine.Specifications.DevelopWithPassion.Rhino;
using Rhino.Mocks;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class FieldSwitcherSpecs
    {
        public abstract class concern : Observes<FieldSwitcher, DefaultFieldSwitcher>
        {
            protected static MemberTarget target;

            Establish c = () =>
                target = the_dependency<MemberTarget>();
        }

        [Subject(typeof (FieldSwitcher))]
        public class when_constructed : concern
        {
            Establish c = () =>
                value_to_change_to = "sdfsdf";

            Because b = () =>
                result = sut.to(value_to_change_to);


            It should_use_the_target_to_get_the_original_value = () =>
                target.received(x => x.get_value());

            static PipelineBehaviour result;
            static string value_to_change_to;
        }

        [Subject(typeof (FieldSwitcher))]
        public class when_provided_the_value_to_change_to : concern
        {
            Establish c = () =>
            {
                value_to_change_to = "sdfsdf";
                original_value = "original value";
                target.Stub(x => x.get_value()).Return(original_value);
            };

            Because b = () =>
                result = sut.to(value_to_change_to);


            It should_provide_the_pipeline_pair_that_can_do_the_switching = () =>
            {
                result.start();
                target.received(x => x.change_value_to(value_to_change_to));

                result.finish();
                target.received(x => x.change_value_to(original_value));
            };

            static PipelineBehaviour result;
            static string value_to_change_to;
            static string original_value;
        }
    }
}