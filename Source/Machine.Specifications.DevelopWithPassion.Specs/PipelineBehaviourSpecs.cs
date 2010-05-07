using Machine.Specifications.DevelopWithPassion.Rhino;

namespace Machine.Specifications.DevelopWithPassion.Specs
{
    public class PipelineBehaviourSpecs
    {
        public class concern : Observes<PipelineBehaviour>
        {
        }

        [Subject(typeof (PipelineBehaviour))]
        public class when_told_to_start : concern
        {
            Establish c = () =>
                create_sut_using(() => new PipelineBehaviour(() => context_ran = true, () => teardown_ran = true));

            Because b = () =>
                sut.start();


            It should_only_run_its_context_block = () =>
            {
                context_ran.ShouldBeTrue();
                teardown_ran.ShouldBeFalse();
            };

            static bool context_ran;
            static bool teardown_ran;
        }

        [Subject(typeof (PipelineBehaviour))]
        public class when_told_to_finish : concern
        {
            Establish c = () =>
                create_sut_using(() => new PipelineBehaviour(() => context_ran = true, () => teardown_ran = true));

            Because b = () =>
                sut.finish();


            It should_only_run_its_teardown_block = () =>
            {
                context_ran.ShouldBeFalse();
                teardown_ran.ShouldBeTrue();
            };

            static bool context_ran;
            static bool teardown_ran;
        }
    }
}