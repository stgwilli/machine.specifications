using Machine.Specifications.DevelopWithPassion.Observations;

namespace Machine.Specifications.DevelopWithPassion.Rhino
{
    public abstract class Observes : StaticObservations<RhinoMocksMockFactory>
    {
        
    }

    public class Observes<Contract, ClassUnderTest> :
        InstanceObservations<Contract, ClassUnderTest, RhinoMocksMockFactory> where ClassUnderTest : Contract
    {
    }

    public class Observes<ClassUnderTest> : Observes<ClassUnderTest, ClassUnderTest>
    {
    }
}