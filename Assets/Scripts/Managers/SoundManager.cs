using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Isometric.Utility
{

    public class SoundManager : SingletonDontDestroyMonobehavior<SoundManager>
    {
        AudioSource[] audioSources = new AudioSource[(int)Enums.Sound.EnumCount];

        private void Start()
        {
            Init();
        }
        public void Init()
        {
            // Sound Enum에 존재하는 음악 종류에 대하여 for문 반복으로 게임오브젝트 생성
            // 3D sound를 구현하지 않는 한 최대한의 동시재생 음원 수와 같아야 의미가 맞는다.
            string[] soundNames = System.Enum.GetNames(typeof(Enums.Sound));
            for (int i=0; i< soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = this.transform;
            }

            //bgm 무한재생
            audioSources[(int)Enums.Sound.Bgm].loop = true;
        }

        public void Play(AudioClip audioClip, Enums.Sound type = Enums.Sound.Effect, float pitch = 1.0f)
        {
            if (audioClip == null)
                return;

            if(type == Enums.Sound.Bgm)
            {
                AudioSource audioSource = audioSources[(int)Enums.Sound.Bgm];
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                audioSource.pitch = pitch;
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            else if(type == Enums.Sound.Effect)
            {
                AudioSource audioSource = audioSources[(int)Enums.Sound.Effect];
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(audioClip);
            }
        }

        public void Clear()
        {
            foreach(AudioSource audioSource in audioSources)
            {
                audioSource.Stop();
                audioSource.clip = null;
            }
        }
        
    }

}