using System;

namespace Machine.Specifications.DevelopWithPassion
{
    public interface MockFactory
    {
        Dependency create_stub<Dependency>() where Dependency : class;
        object create_stub(Type type);
    }
}