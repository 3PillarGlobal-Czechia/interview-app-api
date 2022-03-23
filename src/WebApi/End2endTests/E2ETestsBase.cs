using WebApi;

namespace End2EndTests;

public abstract class E2ETestsBase
{
    protected readonly HttpWrapper _wrapper;

    protected E2ETestsBase(MyWebApplicationFactory<Startup> factory)
    {
        _wrapper = new HttpWrapper(factory.CreateClient());
    }
}
