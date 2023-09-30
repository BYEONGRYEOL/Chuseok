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
       


        public void Init()
        {
            jsonSaver = new JsonSaver();
            gameData = new GameData();
            //������ڸ��� CSV ������ �����Ͽ� Json ������ �ִ��� ������ üũ �� ������ ����

            


            //json ������ �� ��������� ������ �ε� ������ �Ѿ�� �ʾ�������
            //�׷��� json ������ �� �ε� �Ǿ����� �ȵǾ����� UI_Loading���� �˻���
        }

        // CSV ������ Json���� ��ȯ �� Json ���ϵ��� �о� �����ͷ� �޾ƿ� �����ϱ� ������ Json ���� �� ������� �Ŀ� ������ ����ǰų� �ƴϸ�
        // �ش� ���������� �ʿ��� �ε��� �� �� �������� ���Ӿ����� �Ѿ������ �� ������ �Ѵ�.
        // �ڷ�ƾ���� ���� �׼� �������� �ƹ�ư �񵿱�� �� �ʿ��ѵ� �� ��Ƴ�

        
        
        

        
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