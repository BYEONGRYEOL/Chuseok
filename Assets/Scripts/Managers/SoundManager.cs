using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Isometric.Utility
{

    public class SoundManager
    {
        AudioSource[] audioSources = new AudioSource[(int)Enums.Sound.EnumCount];

        private void Start()
        {
            Init();
        }
        public void Init()
        {
            // 배경음악과, 효과음 두개의 오디오소스를 생성한다.
            GameObject root = GameObject.Find("@Sound");
            if (root == null)
            {
                root = new GameObject { name = "@Sound" };
                UnityEngine.Object.DontDestroyOnLoad(root);

                string[] soundNames = Enum.GetNames(typeof(Enums.Sound));
                for(int i = 0; i < soundNames.Length - 1; i++)
                {
                    GameObject go = new GameObject { name = soundNames[i] };
                    // 주의할 점은 동적 할당만 해준다고 되는게 아니라, 유니티 컴포넌트이기때문에, 아래 코드가 필수이다.
                    audioSources[i] = go.AddComponent<AudioSource>();
                    go.transform.parent = root.transform;
                }

                audioSources[(int)Enums.Sound.Bgm].loop = true;
            }



        }

        // 이름을 입력받아 해당 이름을 리소스 디렉토리에서 찾아 재생하는 함수,
        public void Play(string name, Enums.Sound type = Enums.Sound.Effect, float pitch = 1.0f)
        {
            
            if(type == Enums.Sound.Bgm)
            {
                AudioClip audioClip = Resources.Load<AudioClip>("BGM/" + name);
                if (audioClip == null)
                {
                    Debug.Log("audioclip null");
                    return;
                }


                AudioSource audioSource = audioSources[(int)Enums.Sound.Bgm];
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                audioSource.pitch = pitch;
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            else
            {
                AudioClip audioClip = Resources.Load<AudioClip>("SFX/" + name);
                AudioSource audioSource = audioSources[(int)type];
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(audioClip);
            }
        }
        // 현재 재생중인 효과음을 멈추는 함수
        public void StopSFX()
        {
            audioSources[(int)Enums.Sound.Effect].Stop();
        }

        // 다른 씬으로 이동하거나 할때 오디오소스들의 클립을 모두 지우는 함수
        public void Clear()
        {
            foreach(AudioSource audioSource in audioSources)
            {
                audioSource.Stop();
                audioSource.clip = null;
            }
        }
        
        //
        public void MuteAll()
        {
            // 현재 mute되어있다면
            if(audioSources[(int)Enums.Sound.Effect].mute || audioSources[(int)Enums.Sound.Bgm].mute)
            {
                //mute 해제
                audioSources[(int)Enums.Sound.Effect].mute = false;
                audioSources[(int)Enums.Sound.Bgm].mute = false;
            }
            else
            {
                // 아니면 mute하자
                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.mute = true;
                }
            }
            

        }
    }

}