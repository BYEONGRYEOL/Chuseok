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
        

        // json방식으로 save하기위하여 필요한 json saver
        public JsonSaver jsonSaver;
        public GameData gameData;

        // CSV리소스 데이터들을 관리하기 위함
       


        public void Init()
        {
            jsonSaver = new JsonSaver();
            gameData = new GameData();
            
        }

        
        public void MakeJsontoList()
        {
            // 리스트 형식의 게임 리소스데이터를 들고있으려면 이걸실행
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