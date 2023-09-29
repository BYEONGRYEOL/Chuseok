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
        
        //savefile�� �̸��� �����ϴ� �Լ�
        public static string GetSaveFilename(string filename)
        {
            return Application.persistentDataPath + "/" + filename + ".sav";
        }
        public void Save(Dictionary<int, Data> dictionary, string filename)
        {

        }
        public void Save(GameData data, string filename)
        {
            //�ؽ��� ����
            data.hashValue = String.Empty;
            //savedata�� �������� �Ҵ�� ������ jsonutility�� ���� json���� �޴´�.
            string json = JsonUtility.ToJson(data);
            // hashvalue�� json string ���� SHA256 �ڵ带 �Ҵ�
            data.hashValue = GetSHA256(json);
            // 
            json = JsonUtility.ToJson(data);
            
            string path = GetSaveFilename(filename);
            //SAVEFILE ����
            FileStream filestream = new FileStream(path, FileMode.Create);
            Debug.Log(path);
            //������ SaveFile�� saveData�� �ۼ��ϴ� StreamWriter�� ����, ������ json�����ͷ� ������ json ���ڿ��� ����. (json ���ڿ��� Dictionary ������ string �������� �����Ͱ� �����)
            using (StreamWriter writer = new StreamWriter(filestream))
            {
                writer.Write(json);
            }
        }
        // ������ �ε�
        public bool Load(GameData data, string filename)
        {
            //Isometric.sav ������ loadfilename���� �޾Ƽ�
            string loadFilename = GetSaveFilename(filename);
            //�ش� ������ �̹� �����Ѵٸ�, �� ���̺��� ���� �ִٸ�.
            if (File.Exists(loadFilename))
            {
                //StreamReader�� �̿��Ͽ� �ش� ������ ������ �о� json ���ڿ��� �޾ƿ´�.
                using(StreamReader reader = new StreamReader(loadFilename))
                {
                    string json = reader.ReadToEnd();

                    // chech hash before reading
                    // �������� hash���� üũ�Ͽ� ������ ���ٸ�,
                   
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
        //������ ���� ���� �ڵ�
        public void Delete(string filename)
        {
            File.Delete(GetSaveFilename(filename));
        }
        //�������� SHA256 �ڵ� �񱳸� ���� hash���� ������ �������� check
        private bool CheckData(string json)
        {
            GameData tempSaveData = new GameData();
            JsonUtility.FromJsonOverwrite(json, tempSaveData);

            //üũ�ϰ����ϴ� ���̺굥���� json������ savedata�� overwrite�� ��������� hashvalue
            string oldHash = tempSaveData.hashValue;
            tempSaveData.hashValue = String.Empty;

            // üũ�ϰ����ϴ� ���̺굥������ json ������ �����ͷ� hash���� ����� ���
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

