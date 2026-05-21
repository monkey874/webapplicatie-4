using System.Text.Json;

namespace WebApplication4.custumDebugger;

public class CustumDebugger
{
    private static Debugger? Load()
    {
        var json  = File.ReadAllText("DebuggerSettings.json");
        var file = JsonSerializer.Deserialize<Debugger>(json);
        return file ?? null;
    }

    private static (List<string> name, List<string> status) DebuggerSettings()
    {
        var settings = Load();
        
        List<string> debuggerName = [];
        List<string> debuggerStatus = [];
        
        foreach (var i in settings!.DebuggerInfo)
        {
            var debuggerNameString = i.DebuggerSelection.ToString();
            var debuggerStatusString = i.DebuggerStatus.ToString();
        
            debuggerStatus.Add(debuggerStatusString);
            debuggerName.Add(debuggerNameString);
        }
        
        return (debuggerName, debuggerStatus);
    }

    public static void Debugger(int selection, string item)
    {
        var (debuggerName, debuggerStatus) = DebuggerSettings();
        var debuggerNameInt =  debuggerName.ConvertAll(int.Parse);
        var debuggerStatusBool = debuggerStatus.ConvertAll(bool.Parse);

        if (!debuggerNameInt.Contains(selection)) return;
        var index = debuggerNameInt.IndexOf(selection);
        var debuggerStatusInt = debuggerStatusBool[index];
        if (!false == debuggerStatusInt)
        {
            Console.WriteLine(item);
        }
    }
}