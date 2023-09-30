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

    
}