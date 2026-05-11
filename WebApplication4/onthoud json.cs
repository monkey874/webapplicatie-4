namespace WebApplication4;

public class onthoud_json
{
    public void riteFile(string data)
    {
        File.WriteAllText("test.txt",data);
    }
    public string readFile()
    {
        var tekst = File.ReadAllText("test.txt");
        return tekst;
    }
    
    public void DeleteFile()
    {
        File.Delete("test.txt");
    }
}