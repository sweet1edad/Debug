using System;

public class DebugCommand<T1> : DebugCommandBase
{
    private Action<T1> _command;
    public DebugCommand(string id, Action<T1> command) : base(id)
    {
        _command = command;
    }

    public void Invoke(T1 value)
    {
        _command.Invoke(value);
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action _command;
    public DebugCommand(string id, Action command) : base(id)
    {
        _command = command;
    }

    public void Invoke()
    {
        _command.Invoke();
    }
}