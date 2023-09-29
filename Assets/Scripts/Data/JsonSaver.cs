using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;

namespace Isometric.Data 
{

    public class JsonSaver
    {
        
        //savefile의 이름을 리턴하는 함수
        public static string GetSaveFilename(string filename)
        {
            return Application.persistentDataPath + "/" + filename + ".sav";
        }
        public void Save(Dictionary<int, Data> dictionary, string filename)
        {

        }
        public void Save(GameData data, string filename)
        {
            //해쉬값 선언
            data.hashValue = String.Empty;
            //savedata의 변수값과 할당된 값들을 jsonutility를 통해 json으로 받는다.
            string json = JsonUtility.ToJson(data);
            // hashvalue에 json string 값의 SHA256 코드를 할당
            data.hashValue = GetSHA256(json);
            // 
            json = JsonUtility.ToJson(data);
            
            string path = GetSaveFilename(filename);
            //SAVEFILE 생성
            FileStream filestream = new FileStream(path, FileMode.Create);
            Debug.Log(path);
            //생성한 SaveFile에 saveData를 작성하는 StreamWriter를 선언, 이전에 json데이터로 변경한 json 문자열을 쓴다. (json 문자열은 Dictionary 내용을 string 형식으로 적은것과 비슷함)
            using (StreamWriter writer = new StreamWriter(filestream))
            {
                writer.Write(json);
            }
        }
        // 데이터 로드
        public bool Load(GameData data, string filename)
        {
            //Isometric.sav 파일을 loadfilename으로 받아서
            string loadFilename = GetSaveFilename(filename);
            //해당 파일이 이미 존재한다면, 즉 세이브한 적이 있다면.
            if (File.Exists(loadFilename))
            {
                //StreamReader를 이용하여 해당 파일의 끝까지 읽어 json 문자열로 받아온다.
                using(StreamReader reader = new StreamReader(loadFilename))
                {
                    string json = reader.ReadToEnd();

                    // chech hash before reading
                    // 데이터의 hash값을 체크하여 문제가 없다면,
                   
                    if (CheckData(json))
                    {
                        JsonUtility.FromJsonOverwrite(json, data);
                    }
                    else
                    {
                        Debug.Log("Scirpt JsonSaver Function Load : Invalid hashcode");
                    }
                }
                return true;
            }
            return false;
        }
        //데이터 파일 삭제 코드
        public void Delete(string filename)
        {
            File.Delete(GetSaveFilename(filename));
        }
        //데이터의 SHA256 코드 비교를 통해 hash값의 변경이 없었는지 check
        private bool CheckData(string json)
        {
            GameData tempSaveData = new GameData();
            JsonUtility.FromJsonOverwrite(json, tempSaveData);

            //체크하고자하는 세이브데이터 json파일을 savedata로 overwrite한 결과에서의 hashvalue
            string oldHash = tempSaveData.hashValue;
            tempSaveData.hashValue = String.Empty;

            // 체크하고자하는 세이브데이터의 json 형식의 데이터로 hash값을 계산한 결과
            string tempJson = JsonUtility.ToJson(tempSaveData);
            string newHash = GetSHA256(tempJson);

            return (oldHash == newHash);

        }
        public string GetHexStringFromHash(byte[] hash)
        {
            string hexString = String.Empty;
            
            foreach(byte b in hash)
            {
                hexString += b.ToString("x2");
            }
            return hexString;
        }

        private string GetSHA256(string text)
        {
            byte[] texttoBytes = Encoding.UTF8.GetBytes(text);
            
            SHA256Managed mySHA256 = new SHA256Managed();

            byte[] hashValue = mySHA256.ComputeHash(texttoBytes);
            
            return GetHexStringFromHash(hashValue);
        }

        
    }

}

