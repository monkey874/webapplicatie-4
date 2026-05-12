using System.IO;
namespace Stoplichtsysteem.JsonLoader
{
    public class JsonLoader
    {
        public string LoadJsonFiles(string jsonFile)
        {
            var file = jsonFile;
            var json = File.ReadAllText(file);
            return json;
        }
    }
}