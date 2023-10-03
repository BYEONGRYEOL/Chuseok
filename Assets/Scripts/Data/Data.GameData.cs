using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Isometric.Data
{
    [Serializable]
    public class GameData : Data
    {
        private bool isMobile;
        public bool IsMobile
        {
            get => isMobile;
            set => isMobile = value;
        }

        private bool isMute;
        private float sfxVolume;
        private float musicVolume;


        public bool IsMute { get => isMute; set => IsMute = value; }
        public float SFXVolume { get => sfxVolume; set => sfxVolume = value; }
        public float MusicVolume { get => musicVolume; set => musicVolume = value; }



        public GameData()
        {
            hashValue = string.Empty;
            isMobile = false;
            isMute = false;
            sfxVolume = 0f;
            musicVolume = 0f;
        }
    }

    
}