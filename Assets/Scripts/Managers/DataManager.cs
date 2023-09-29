using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace Isometric.Data
{
    // �ε��� �����Ϳ� ���Ͽ� 
    public interface ILoaderDict<Key, Value> 
    {
        Dictionary<Key, Value> MakeDict();
    }
    public interface ILoaderList<list> 
    {
        List<list> MakeList();
    }


    public class DataManager
    {
        
        // �÷��̾� �������� ���� �������� stat�� ���� ���� CSV���Ϸ� �����Ͽ� �����ϱ� �����ϰ� ����
        // �׷��� ���� ��  �÷��̾ ����־����� ���� ���� ������ �߻��� �Ͽ����Ͽ� save �ϴ°� Json ������ ����ϴ�.
        // �׷��� �⺻���� Stat, Ȥ�� ������ Quest, ������� ���� �����͸� CSV�� ���� �ۼ��� �� Json���� ��ȯ���ִ� Util �Լ��� �ʿ��ҰŰ���.
        // �׷��� ���� ù ���۽� CSV to json ��ȯ Ȥ�� �������� ������Ʈ �� ���� �����ϴϱ� ���� Util �Լ��� ������� ����~~


        // json������� save�ϱ����Ͽ� �ʿ��� json saver
        public JsonSaver jsonSaver;
        public GameData gameData;

        // CSV���ҽ� �����͵��� �����ϱ� ����
        public Dictionary<int, PlayerStatData> PlayerStatDict { get; private set; } = new Dictionary<int, PlayerStatData>();
        public Dictionary<int, EnemyStatData> EnemyStatDict { get; private set; } = new Dictionary<int, EnemyStatData>();
        public Dictionary<int, ItemInfo> ItemInfoDict { get; private set; } = new Dictionary<int, ItemInfo>();
        public Dictionary<int, WeaponInfo> WeaponInfoDict { get; private set; } = new Dictionary<int, WeaponInfo>();
        public Dictionary<int, ArmorInfo> ArmorInfoDict { get; private set; } = new Dictionary<int, ArmorInfo>();
        public Dictionary<int, ConsumableInfo> ConsumableInfoDict { get; private set; } = new Dictionary<int, ConsumableInfo>();
        public Dictionary<int, UseableInfo> UseableInfoDict { get; private set; } = new Dictionary<int, UseableInfo>();
        public Dictionary<int, ItemData> ItemDBDict { get; private set; } = new Dictionary<int, ItemData>();


        public void Init()
        {
            jsonSaver = new JsonSaver();
            gameData = new GameData();
            //������ڸ��� CSV ������ �����Ͽ� Json ������ �ִ��� ������ üũ �� ������ ����

            //CSV ���Ϸ� �����ϴ� ���ҽ������Ϳ� ���Ͽ� ����
            MakeJson(Datas.EnemyStat.ToString());
            MakeJson(Datas.PlayerStat.ToString());
            MakeJson(Datas.ItemInfo.ToString());
            MakeJson(Datas.WeaponInfo.ToString());
            MakeJson(Datas.ArmorInfo.ToString());
            MakeJson(Datas.ConsumableInfo.ToString());
            MakeJson(Datas.UseableInfo.ToString());




            //json ������ �� ��������� ������ �ε� ������ �Ѿ�� �ʾ�������
            //�׷��� json ������ �� �ε� �Ǿ����� �ȵǾ����� UI_Loading���� �˻���
        }

        // CSV ������ Json���� ��ȯ �� Json ���ϵ��� �о� �����ͷ� �޾ƿ� �����ϱ� ������ Json ���� �� ������� �Ŀ� ������ ����ǰų� �ƴϸ�
        // �ش� ���������� �ʿ��� �ε��� �� �� �������� ���Ӿ����� �Ѿ������ �� ������ �Ѵ�.
        // �ڷ�ƾ���� ���� �׼� �������� �ƹ�ư �񵿱�� �� �ʿ��ѵ� �� ��Ƴ�

        public enum Datas
        {
            PlayerStat,
            EnemyStat,
            ItemInfo,
            WeaponInfo,
            ArmorInfo,
            ConsumableInfo,
            UseableInfo,
            ItemDB
        }
        
        //json ���� ������
        public bool MakeJson(string csvName)
        {
            return CSVtoJson.ConvertCsvFileToJsonObject(csvName);
        }

        public bool IsJsonLoaded()
        {
            List<bool> a = new List<bool>();
            foreach (Datas datas in Enum.GetValues(typeof(Datas)))
            {
                a.Add(MakeJson(datas.ToString()));
            }
            if (a.Contains(false))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void MakeJsontoDict()
        {
            Debug.Log("Datamanager : MakeJsontoDict ����");
            PlayerStatDict = LoadJson<PlayerStatLoader, int, PlayerStatData>("PlayerStatjson").MakeDict();
            EnemyStatDict = LoadJson<EnemyStatLoader, int, EnemyStatData>("EnemyStatjson").MakeDict();
            ItemInfoDict = LoadJson<ItemInfoData, int, ItemInfo>("ItemInfojson").MakeDict();
            WeaponInfoDict = LoadJson<WeaponInfoData, int, WeaponInfo>("WeaponInfojson").MakeDict();
            ArmorInfoDict = LoadJson<ArmorInfoData, int, ArmorInfo>("ArmorInfojson").MakeDict();
            ConsumableInfoDict = LoadJson<ConsumableInfoData, int, ConsumableInfo>("ConsumableInfojson").MakeDict();
            UseableInfoDict = LoadJson<UseableInfoData, int, UseableInfo>("UseableInfojson").MakeDict();
            ItemDBDict = LoadJson<ItemLoader, int, ItemData>("ItemDBjson").MakeDict();

            Debug.Log("ItemDBDict �ε� :: " + ItemDBDict.Count);
        }

        public void MakeJsontoList()
        {
            // ����Ʈ ������ ���� ���ҽ������͸� ����������� �̰ɽ���
        }

        //Resouce/Data/���⿡ �����͸� �޾ƿ� �ش� CSV ������ �־����
        Loader LoadJson<Loader, list>(string path) where Loader : ILoaderList<list>
        {
            TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
            return JsonUtility.FromJson<Loader>(textAsset.text);
        }
        Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoaderDict<Key, Value>
        {
            TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
            return JsonUtility.FromJson<Loader>(textAsset.text);
        }
        

        public void Save()
        {
            jsonSaver.Save(gameData, "GameData");

        }
        public void Load()
        {
            jsonSaver.Load(gameData, "GameData");

        }

    }

}