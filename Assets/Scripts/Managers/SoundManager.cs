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
            // Sound Enum�� �����ϴ� ���� ������ ���Ͽ� for�� �ݺ����� ���ӿ�����Ʈ ����
            // 3D sound�� �������� �ʴ� �� �ִ����� ������� ���� ���� ���ƾ� �ǹ̰� �´´�.
            string[] soundNames = System.Enum.GetNames(typeof(Enums.Sound));
            for (int i=0; i< soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = this.transform;
            }

            //bgm �������
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