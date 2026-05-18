namespace WebApplication4.Debuger;

public class Debugger
{
    public required List<DebuggerInfo> DebuggerInfo { get; init; }
        
}

public class DebuggerInfo
{
    public required int DebuggerSelection { get; init; }
    public required bool DebuggerStatus {get; init; }
}