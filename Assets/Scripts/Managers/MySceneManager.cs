using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Isometric
{

    public class MySceneManager
    {
        //���� ������ SceneBase ��ũ��Ʈ�� ��ӹް��ִ� ���ӿ�����Ʈ�� �� ���� ���� ( �� ���� �� ���� �����ϱ���߰ŵ�)
        // �׷��� �׳� �� ������ SceneBase type�� ���ӿ�����Ʈ�� �޾ƿ��� ������ ���� ���� ������ üũ�� �� �ִ�.
        public SceneBase CurrentScene { get { return GameObject.FindObjectOfType<SceneBase>(); } }
        public void LoadScene(Enums.Scene type)
        {
            //Enums �� �̿��Ͽ� ���� �ε带 ����
            // �ٸ� ���� �ε��ϱ������� �� ���� ���� Clear �Լ��� �����ؾ��Ѵ�.
            CurrentScene.Clear();
            SceneManager.LoadScene(GetSceneName(type));
        }

        // Enums.Scene enum�� �����Ͽ�, ���� �̸��� ��ȯ�ϴ� �Լ�.
        string GetSceneName(Enums.Scene type)
        {
            string name = System.Enum.GetName(typeof(Enums.Scene), type);
            return name;
        }
    }

}