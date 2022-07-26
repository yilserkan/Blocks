using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public static class JSONSaveSystem
{
    public static string filename = "levels.json";
    
    // This class is hanlding the JSON operations.
    // There is another JSON helper class which is being used in this class
    // The JSON helper class is basically creating a wrapper around all entries in the JSON file
    // This way we can easily get a List of all datas inside the wrapper. 
    public static void SaveToJSON<T>(List<T> saveData, bool prettyPrint)
    {
        List<T> dataInJson = ReadRomJson<T>();
        string content = JsonHelper.ToJson(saveData, prettyPrint, dataInJson);
        WriteFile(GetPath(), content);
    }
    
    public static List<T> ReadRomJson<T> ()
    {
        string content = ReadFile(GetPath());
        if (string.IsNullOrEmpty(content) || content == "")
        {
            return new List<T>();
        }

        List<T> res = JsonHelper.FromJson<T>(content).ToList();
        
        return res;
    }
    
    private static string GetPath()
    {
        return Application.dataPath + "/JSON Files/" + filename;
    }
    
    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
     
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }

        return "";
    }
}