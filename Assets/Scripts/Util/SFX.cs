/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric;
using Isometric.Data;
using Isometric.Utility;
//SFX only manages sounds that plays only one at a time
public class SFX: SingletonDontDestroyMonobehavior<SFX>
{
    private AudioSource audioSource;
    [SerializeField] public AudioClip audio_Btnclick;
    [SerializeField] public AudioClip audio_BtnBackclick;

    [SerializeField] public AudioClip audio_Win;
    [SerializeField] public AudioClip audio_Drawing;
    [SerializeField] public AudioClip audio_Drawing2;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        if (DataManager_old.Instance.Mute)
        {
            Mute();
        }
        Debug.Log("Loading Volume" + DataManager_old.Instance.SFXVolume);
        Volume(DataManager_old.Instance.SFXVolume);

    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
    public void StopAudio()
    {
        audioSource.Stop();
    }
    public void PlaySFX(string clipName)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        SwitchClip(clipName);
        audioSource.Play();
    }
    private void SwitchClip(string clipName)
    {
        switch (clipName) 
        {
            case "BtnClick":
                Debug.Log("버튼클릭");
                audioSource.clip = audio_Btnclick;
                break;
            case "Win":
                Debug.Log("승리");

                audioSource.clip = audio_Win;
                break;
            case "Drawing":
                audioSource.clip = audio_Drawing;
                break;
            case "Drawing2":
                audioSource.clip = audio_Drawing2;
                break;
            case "BtnBackClick":
                Debug.Log("뒤로가기");

                //수정고민중
                audioSource.clip = audio_BtnBackclick;
                break;
        }
    }
    public void Volume(float vol)
    {
        Debug.Log("Script :: BGM / Function :: Volume");
        audioSource.volume = vol;
    }
    public void UnMute()
    {
        audioSource.mute = false;
        audioSource.Play();
    }
    public void Mute()
    {
        audioSource.mute = true;
        audioSource.Stop();
    }
}
*/