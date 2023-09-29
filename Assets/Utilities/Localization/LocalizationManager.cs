using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Text;


namespace Isometric.Utility
{

    public class LocalizationManager : SingletonDontDestroyMonobehavior<LocalizationManager>
    {
        private LocalizationData localizationData;

        private Dictionary<string, string> localizedText;
        private string missingTextString = "text not found";
        public string localLanguage = "en";
        private int localizingStep = 0;
        public void LoadCSVToJson(string fileName)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
            filePath += ".csv";
            if (File.Exists(filePath)) 
            { 
                string dataAsJson = File.ReadAllText(filePath, Encoding.UTF8); 
                string[] stringBigList = dataAsJson.Split('\n'); 
                localizationData = new LocalizationData(); 
                localizationData.items = new LocalizationItem[stringBigList.Length]; 
                for (var i = 1; i < stringBigList.Length -1; i++) 
                {
                    Debug.Log("stringBigList is " + stringBigList[i]);
                    
                    string[] stringList = stringBigList[i].Split(',');
                    Debug.Log("stringList is " + stringList[1]);
                    for (var j = 0; j < stringList.Length; j++) 
                    { 
                        localizationData.items[i - 1] = new LocalizationItem(stringList[1], stringList[2]); 
                    } 
                }
            }
            localizingStep = 1;
            Debug.Log("Loadcsv 완료");
        }
        
        private void SaveJsonToTxt(string fileName) 
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
            filePath += ".txt";
            string dataAsJson = JsonUtility.ToJson(localizationData);
            File.WriteAllText(filePath, dataAsJson, Encoding.UTF8);
            localizingStep = 2;
            Debug.Log("save json 완료");

        }




        private void Start()
        {
            CheckSystemLanguage();
            string fileName = "LocalizedText_" + localLanguage;
            StartCoroutine(Localization(fileName));
        }
        public void SetLocalLanguage(string lang)
        {
            localLanguage = lang;
            string fileName = "LocalizedText_" + localLanguage;
            StartCoroutine(Localization(fileName));


        }
        IEnumerator Localization(string fileName)
        {
            localizingStep = 0;
            LoadCSVToJson(fileName);

            yield return new WaitWhile(() => localizingStep < 1);

            SaveJsonToTxt(fileName);
            yield return new WaitWhile(() => localizingStep < 2);
            LoadTXTToLocalizedText(fileName);
        }

        public void CheckSystemLanguage()
        {
            SystemLanguage systemLanguage = Application.systemLanguage;
            
            switch (systemLanguage) 
            {
                case SystemLanguage.Korean:
                    localLanguage = "ko";
                    break;
                default:
                    localLanguage = "en";
                    break;
            }
            localizingStep = 0;
            
        }


        
        public void LoadTXTToLocalizedText(string fileName)
        {
            localizedText = new Dictionary<string, string>();
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
            filePath += ".txt";
            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
                Debug.Log(loadedData);
                for (int i = 0; i < loadedData.items.Length-2; i++)
                {
                    Debug.Log(loadedData.items[i].key);

                    localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
                }
                Debug.Log("Data loaded. Dictionary contains : " + localizedText.Count + "entries");

            }
            else
            {
                Debug.LogError("file not found");
            }

            Debug.Log("load txt 완료");

        }

        public string GetLocalizedValue(string key)
        {
            Debug.Log(localizedText);
            Debug.Log(localizedText.Count);
            string result = missingTextString;
            
            result = localizedText[key];
            if (localizedText.ContainsKey(key))
            {
                result = localizedText[key];
            }
            return result;
        }
    }

    
}