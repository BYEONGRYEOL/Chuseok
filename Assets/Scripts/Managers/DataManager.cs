using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace Isometric.Data
{
    

    public class DataManager
    {
        

        // json������� save�ϱ����Ͽ� �ʿ��� json saver
        public JsonSaver jsonSaver;
        public GameData gameData;

        // CSV���ҽ� �����͵��� �����ϱ� ����
       


        public void Init()
        {
            jsonSaver = new JsonSaver();
            gameData = new GameData();
            
        }

        
        public void MakeJsontoList()
        {
            // ����Ʈ ������ ���� ���ҽ������͸� ����������� �̰ɽ���
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