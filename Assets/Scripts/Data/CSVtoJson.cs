using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;



public class CSVtoJson
{
    public static bool ConvertCsvFileToJsonObject(string filename)
    {
        string json_name = filename + "json";
        string json_path = $"C:/Users/sbl/Isometric/Assets/Resources/Data/{json_name}.json";
        string csv_path = $"C:/Users/sbl/Isometric/Assets/Resources/Data/{filename}.csv";
        if (File.Exists(json_path))
        {
            Debug.Log($"{json_name}.json already exists");
            return true;
        }

        var csv = new List<string[]>();

        var lines = File.ReadAllLines(csv_path);

        foreach (string line in lines)
            csv.Add(line.Split(','));

        var properties = lines[0].Split(',');
         
        var listObjResult = new List<Dictionary<string, string>>();
        for (int i = 1; i < lines.Length; i++)
        {
            var objResult = new Dictionary<string, string>();
            for (int j = 0; j < properties.Length; j++)
                objResult.Add(properties[j], csv[i][j]);

            listObjResult.Add(objResult);
        }
        Dictionary<string, List<Dictionary<string, string>>> result = new Dictionary<string, List<Dictionary<string, string>>>();

        result.Add(filename, listObjResult);

        using (StreamWriter file = File.CreateText(json_path))
        {
            JsonSerializer serializer = new JsonSerializer();
            //serialize object directly into file stream
            serializer.Serialize(file, result);
        }
        return true;
        

    }
}
