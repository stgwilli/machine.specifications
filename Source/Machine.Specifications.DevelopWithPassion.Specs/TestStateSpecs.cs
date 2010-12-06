 using System.Collections.Generic;
 using Machine.Specifications;
 using Machine.Specifications.DevelopWithPassion.Rhino;

namespace Machine.Specifications.DevelopWithPassion.Specs
{   
    public class TestStateSpecs
    {
        public abstract class concern : Observes<TestState<SomeItem>,
                                            DefaultTestState<SomeItem>>
        {
        
        }

        [Subject(typeof(DefaultTestState<SomeItem>))]
        public class when_a_pipleine_action_has_been_registered_against_the_system_under_test : concern
        {
            Establish c = () =>
            {
                some_item = new SomeItem();
                factory = () => some_item;
                create_sut_using(() => new DefaultTestState<SomeItem>(factory,new List<PipelineBehaviour>()));
                add_pipeline_behaviour_against_sut(x => x.add_behaviour_against_sut(y => y.was_leveraged =true));
            };

            It should_have_ran_that_action_before_the_because_block_run = () =>
            {
                some_item.was_leveraged = true;
            };

            static SomeItem some_item;
            static SUTFactory<SomeItem> factory;
        }
    }

    public class SomeItem
    {
        public bool was_leveraged;
    }
}
