namespace Machine.Specifications.DevelopWithPassion.Observations
{
    public abstract class InstanceObservationsWithContract<Contract, ClassUnderTest,MockingFactory> : InstanceObservations<Contract, ClassUnderTest,MockingFactory>
        where ClassUnderTest : Contract where MockingFactory : MockFactory, new()
    {

    }
}