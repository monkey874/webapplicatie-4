namespace WebApplication4.custumDebugger;

public class Debugger
{
    public required List<DebuggerInfo> DebuggerInfo { get; init; }
}

public class DebuggerInfo
{
    public required int DebuggerSelection { get; init; }
    public required bool DebuggerStatus {get; init; }
}