using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Isometric.Data
{
    [Serializable]
    public class GameData : Data
    {
        private int inventory_capacity;
        private float playingTime;
        private float ingameTime;
        public int Inventory_capacity { get => inventory_capacity; set => inventory_capacity = value; }
        public float PlayingTime { get=> playingTime; set => playingTime = value; }
        public float IngameTime { get => ingameTime; set => ingameTime = value; }


        private bool tutorialCompleted;
        private string id;
        private string password;
        private readonly string defaultID = "ID";
        private readonly string defaultPassword = "Password";

        private bool isMute;
        private float sfxVolume;
        private float musicVolume;
        private List<string> keyBindKeys;
        private List<KeyCode> keyBindValues;

        private List<string> actionBindKeys;
        private List<KeyCode> actionBindValues;

        public bool TutorialCompleted { get => tutorialCompleted; set => tutorialCompleted = value; }
        public string Id { get => id; set => id = value; }
        public string Password { get => password; set => password = value; }
        public bool IsMute { get => isMute; set => IsMute = value; }
        public float SFXVolume { get => sfxVolume; set => sfxVolume = value; }
        public float MusicVolume { get => musicVolume; set => musicVolume = value; }
        public List<string> KeyBindKeys { get => keyBindKeys; set => keyBindKeys = value; }
        public List<KeyCode> KeyBindValues { get => keyBindValues; set => keyBindValues = value; }
        public List<string> ActionBindKeys { get => actionBindKeys; set => actionBindKeys = value; }
        public List<KeyCode> ActionBindValues { get => actionBindValues; set => actionBindValues = value; }



        public GameData()
        {
            hashValue = string.Empty;

            ingameTime = 0f;
            playingTime = 0f;
            inventory_capacity = 20;

            tutorialCompleted = false;
            id = defaultID;
            password = defaultPassword;
            isMute = false;
            sfxVolume = 0f;
            musicVolume = 0f;

            keyBindKeys = new List<string>();
            keyBindValues = new List<KeyCode>();
            actionBindKeys = new List<string>();
            actionBindValues = new List<KeyCode>();
        }
    }

    [Serializable]
    public class ItemData : Data
    {
        public int itemDbID;
        public int itemTemplateID;
        public string name;
        public string description;
        public int count;
        public int slot;
        public Enums.ItemType itemType;
    }
    [Serializable]
    public class WeaponData : ItemData 
    {
        public Enums.WeaponType weaponType;
        public int attack;
        
    }
    [Serializable]
    public class ArmorData : ItemData
    {
        public Enums.ArmorType armorType;
        public int defense;
    }
    [Serializable]
    public class ConsumableData : ItemData
    {
        
        public Enums.ConsumableType consumableType;
        public float hp;
        public Enums.BuffType buffType;
    }
    [Serializable]
    public class UseableData : ItemData
    {
        public Enums.UseableType useableType;
    }

    [Serializable]
    public class ItemLoader : ILoaderDict<int, ItemData>
    {
        public List<WeaponData> WeaponDB = new List<WeaponData>();
        public List<ArmorData> ArmorDB = new List<ArmorData>();
        public List<ConsumableData> ConsumableDB = new List<ConsumableData>();
        public List<UseableData> UseableDB = new List<UseableData>();

        public Dictionary<int, ItemData> MakeDict()
        {
            Debug.Log("ItemDBData MakeDict ½ÇÇà");
            Dictionary<int, ItemData> ItemDB = new Dictionary<int, ItemData>();
            foreach(ItemData item in WeaponDB)
            {
                item.itemType = Enums.ItemType.Weapon;
                ItemDB.Add(item.itemDbID, item);
            }
            foreach (ItemData item in ArmorDB)
            {
               
                item.itemType = Enums.ItemType.Armor;
                ItemDB.Add(item.itemDbID, item);

            }
            foreach (ItemData item in ConsumableDB)
            {
               
                item.itemType = Enums.ItemType.Consumable;
                ItemDB.Add(item.itemDbID, item);

            }
            foreach (ItemData item in UseableDB)
            {
                
                item.itemType = Enums.ItemType.Useable;
                ItemDB.Add(item.itemDbID, item);

            }

            return ItemDB;
        }
    }
}