using WebApi;

namespace End2EndTests;

public abstract class E2ETestsBase
{
    protected HttpWrapper wrapper { get; }

    protected E2ETestsBase(MyWebApplicationFactory<Startup> factory)
    {
        wrapper = new HttpWrapper(factory.CreateClient());
    }
}
