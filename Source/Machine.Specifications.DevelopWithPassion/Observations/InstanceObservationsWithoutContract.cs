namespace Machine.Specifications.DevelopWithPassion.Observations
{
    public abstract class InstanceObservationsWithoutContract<SystemUnderTest,MockingFactory> : InstanceObservations<SystemUnderTest, SystemUnderTest,MockingFactory> where MockingFactory : MockFactory, new()
    {
    }
}