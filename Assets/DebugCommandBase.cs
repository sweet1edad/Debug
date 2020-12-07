public class DebugCommandBase
{
    public string Id { get; private set; }

    public DebugCommandBase(string id)
    {
        Id = id;
    }
}