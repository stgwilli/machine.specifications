namespace Machine.Specifications.DevelopWithPassion.Observations
{
    public class ObservationContextFactory 
    {
        public static DefaultObservationContext<Contract> create_an_observation_context_from<Contract>(TestState<Contract> test_state,MockFactory mock_factory)
        {
            var dependency_builder = new DefaultSystemUnterTestDependencyBuilder(test_state,mock_factory);
            return new DefaultObservationContext<Contract>(
                test_state,
                mock_factory,
                dependency_builder,
                new DefaultSystemUnderTestFactory(dependency_builder));
        }
    }
}