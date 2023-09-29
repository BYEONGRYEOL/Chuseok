using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;
using Isometric.Data;
using System;
using System.Linq;
namespace Isometric
{

    public class KeyBindManager
    {
        private string bindName;

        public List<string> keyBindKeys;
        public List<KeyCode> keyBindValues;
        public List<string> actionBindKeys;
        public List<KeyCode> actionBindValues;

        public Dictionary<string, KeyCode> KeyBinds { get; set; }
        public Dictionary<string, KeyCode> ActionBinds { get; set; }
        
        string GetKeyName(Enums.Key type)
        {
            string name = System.Enum.GetName(typeof(Enums.Key), type);
            return name;
        }
        public void BindKey(string key, KeyCode keyBind)
        {
            //Debug.Log("BindKey ����");
            // Keybinds �� ActionBinds�� bind �ϴ°� �ϳ��� �Լ����� �� �� �ְ� ���� ��ųʸ� ����
            Dictionary<string, KeyCode> currentDictionary = KeyBinds;
            //Debug.Log("���� ��ųʸ� ����");
            
            // Action�� ���Ե� ��� ActionBinds 
            if (key.Contains("ACTION_"))
            {
                //Debug.Log("�׼��� �����Ѵٸ�");
                currentDictionary = ActionBinds;
            }
          //  Debug.Log("ù if �� ���");
            // �Է¹��� key �� �ش��ϴ� value�� ���� ��� �׳� �߰���
            if (!currentDictionary.ContainsKey(key))
            {
                //Debug.Log("���� Ű�� ���ٸ�");
                currentDictionary.Add(key, keyBind);
            }
            
            // �Է¹��� key �� �ش��ϴ� ��value�� ������� �ϳ��� ��ư���� �� ���� ����Ű�� ������ �ȵǴϱ� �ٲ����
            else if (currentDictionary.ContainsValue(keyBind))
            {
                //Debug.Log("Ű�� �ش��ϴ� ����� �ִٸ�");
                string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
                //Debug.Log("ã�Ƽ�");
                currentDictionary[myKey] = KeyCode.None;

            }
            //  Debug.Log("Ű�� �´� ��� �ֱ�");
            currentDictionary[key] = keyBind;
            bindName = string.Empty;

            //save

        }

        public void Init()
        {
            KeyBinds = new Dictionary<string, KeyCode>();
            ActionBinds = new Dictionary<string, KeyCode>();

            BindKey("UP", KeyCode.W);
            BindKey("DOWN", KeyCode.S);
            BindKey("RIGHT", KeyCode.D);
            BindKey("LEFT", KeyCode.A);

            BindKey("JUMP", KeyCode.Space);
            BindKey("RUN", KeyCode.LeftShift);
            BindKey("CROUCH", KeyCode.LeftControl);
            BindKey("INTERACTION", KeyCode.G);
            BindKey("ACTION_1", KeyCode.Alpha1);
            BindKey("ACTION_2", KeyCode.Alpha2);
            BindKey("ACTION_3", KeyCode.Alpha3);
            BindKey("ACTION_4", KeyCode.Alpha4);


            
            Managers.Data.gameData.KeyBindKeys = KeyBinds.Keys.ToList();
            Managers.Data.gameData.ActionBindKeys = ActionBinds.Keys.ToList();
            Managers.Data.gameData.KeyBindValues = KeyBinds.Values.ToList();
            Managers.Data.gameData.ActionBindValues = ActionBinds.Values.ToList();
            
        }
        public void SaveData()
        {

            Managers.Data.gameData.KeyBindKeys = KeyBinds.Keys.ToList();
            Managers.Data.gameData.ActionBindValues = ActionBinds.Values.ToList();
            Managers.Data.gameData.ActionBindKeys = ActionBinds.Keys.ToList();
            Managers.Data.gameData.KeyBindValues = KeyBinds.Values.ToList();


            Managers.Data.Save();
            
        }
        public void LoadData()
        {
            KeyBinds = new Dictionary<string, KeyCode>();
            ActionBinds = new Dictionary<string, KeyCode>();

            Managers.Data.Load();

            if (Managers.Data.gameData.KeyBindKeys.Count == 0)
            {
                Debug.Log("Ű ���� �ʱ�ȭ ����");
                Init();
            }
            else
            {
                Debug.Log("������ �ε�");

                keyBindKeys = Managers.Data.gameData.KeyBindKeys;
                keyBindValues = Managers.Data.gameData.KeyBindValues;
                actionBindKeys = Managers.Data.gameData.ActionBindKeys;
                actionBindValues = Managers.Data.gameData.ActionBindValues;


                for (int i = 0; i < keyBindKeys.Count; i++)
                {
                    //Debug.Log(keyBindKeys + "keybindkeys");
                   // Debug.Log(keyBindKeys.Count);
                 //   Debug.Log(keyBindKeys[i]);
                //    Debug.Log(keyBindValues[i]);
                    KeyBinds.Add(keyBindKeys[i], keyBindValues[i]);

                    BindKey(KeyBinds.Keys.ToList()[i], KeyBinds[KeyBinds.Keys.ToList()[i]]);
                }
                for (int i = 0; i < actionBindKeys.Count; i++)
                {
                    ActionBinds.Add(actionBindKeys[i], actionBindValues[i]);

                    BindKey(ActionBinds.Keys.ToList()[i], ActionBinds[ActionBinds.Keys.ToList()[i]]);
                }


            }
        }
    }

}