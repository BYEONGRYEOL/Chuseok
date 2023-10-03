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
            // ������ǰ�, ȿ���� �ΰ��� ������ҽ��� �����Ѵ�.
            GameObject root = GameObject.Find("@Sound");
            if (root == null)
            {
                root = new GameObject { name = "@Sound" };
                UnityEngine.Object.DontDestroyOnLoad(root);

                string[] soundNames = Enum.GetNames(typeof(Enums.Sound));
                for(int i = 0; i < soundNames.Length - 1; i++)
                {
                    GameObject go = new GameObject { name = soundNames[i] };
                    // ������ ���� ���� �Ҵ縸 ���شٰ� �Ǵ°� �ƴ϶�, ����Ƽ ������Ʈ�̱⶧����, �Ʒ� �ڵ尡 �ʼ��̴�.
                    audioSources[i] = go.AddComponent<AudioSource>();
                    go.transform.parent = root.transform;
                }

                audioSources[(int)Enums.Sound.Bgm].loop = true;
            }



        }

        // �̸��� �Է¹޾� �ش� �̸��� ���ҽ� ���丮���� ã�� ����ϴ� �Լ�,
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
        // ���� ������� ȿ������ ���ߴ� �Լ�
        public void StopSFX()
        {
            audioSources[(int)Enums.Sound.Effect].Stop();
        }

        // �ٸ� ������ �̵��ϰų� �Ҷ� ������ҽ����� Ŭ���� ��� ����� �Լ�
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
            // ���� mute�Ǿ��ִٸ�
            if(audioSources[(int)Enums.Sound.Effect].mute || audioSources[(int)Enums.Sound.Bgm].mute)
            {
                //mute ����
                audioSources[(int)Enums.Sound.Effect].mute = false;
                audioSources[(int)Enums.Sound.Bgm].mute = false;
            }
            else
            {
                // �ƴϸ� mute����
                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.mute = true;
                }
            }
            

        }
    }

}