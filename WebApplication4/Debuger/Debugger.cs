using System.Text.Json;
using WebApplication4.Debuger;

namespace DebugerApp;

public class Debuger
{
    public Debugger load()
    {
        var json  = File.ReadAllText("DebuggerSettings.json");
        Debugger file = JsonSerializer.Deserialize<Debugger>(json); 
        return file;
    }

    public (List<string> name, List<string> status) DebuggerSettings()
    {
        var test = load();
        
        List<string> Debuggername = new List<string>();
        List<string> DebuggerStatus = new List<string>();
        
        foreach (var i in test.DebuggerInfo)
        {
            var DebuggerNameString = i.DebuggerSelection.ToString();
            var DebuggerStatusString = i.DebuggerStatus.ToString();
        
            DebuggerStatus.Add(DebuggerStatusString);
            Debuggername.Add(DebuggerNameString);
        }
        
        return (Debuggername, DebuggerStatus);
    }

    public void Debugger(int Selection, string Item)
    {
        var test = DebuggerSettings();
        var Debuggername = test.name;
        var DebuggerStatus = test.status;
        var DebuggernameInt =  Debuggername.ConvertAll(int.Parse);
        var DebuggerStatusBool = DebuggerStatus.ConvertAll(bool.Parse);

        if (DebuggernameInt.Contains(Selection))
        {
            var index = DebuggernameInt.IndexOf(Selection);
            var DebuggerStatusInt = DebuggerStatusBool[index];
            if (!false == DebuggerStatusInt)
            {
                Console.WriteLine(Item);
            }
        }
    }
}