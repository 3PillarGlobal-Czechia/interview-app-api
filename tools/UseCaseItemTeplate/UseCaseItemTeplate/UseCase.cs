namespace $rootnamespace$;

public class $safeitemname$UseCase
{
	private IOutputPort _outputPort;

    public $safeitemname$UseCase()
    {
        this._outputPort = new $safeitemname$Presenter();
    }

    public Task Execute($safeitemname$Input input)
    {
        // TODO
        return Task.FromResult(0);
    }

    public void SetOutputPort(IOutputPort outputPort)
    {
        throw new NotImplementedException();
    }
}

