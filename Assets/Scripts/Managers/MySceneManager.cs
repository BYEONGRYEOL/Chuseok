using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Isometric
{

    public class MySceneManager
    {
        //현재 씬에는 SceneBase 스크립트를 상속받고있는 게임오브젝트가 한 개일 것임 ( 한 씬에 한 개만 생성하기로했거든)
        // 그래서 그냥 씬 내에서 SceneBase type의 게임오브젝트를 받아오는 것으로 현재 무슨 씬인지 체크할 수 있다.
        public SceneBase CurrentScene { get { return GameObject.FindObjectOfType<SceneBase>(); } }
        public void LoadScene(Enums.Scene type)
        {
            //Enums 를 이용하여 씬의 로드를 구현
            // 다른 씬을 로드하기전에는 꼭 현재 씬의 Clear 함수를 실행해야한다.
            CurrentScene.Clear();
            SceneManager.LoadScene(GetSceneName(type));
        }

        // Enums.Scene enum을 참조하여, 씬의 이름을 반환하는 함수.
        string GetSceneName(Enums.Scene type)
        {
            string name = System.Enum.GetName(typeof(Enums.Scene), type);
            return name;
        }
    }

}