using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace Isometric.UI
{

    public class UI_Option : UI_Popup
    {
        enum GameObjects { }
        enum TextMeshProUGUIs 
        {
            Txt_MusicVolume,
            Txt_SFXVolume
        }
        enum Buttons 
        { 
            Btn_Back,
            Btn_KeyBinds,
            Btn_Save
        }
        enum Images { }
        enum Sliders 
        {
            Slider_MusicVolume,
            Slider_SFXVolume
        }

        void Start()
        {
            Init();
        }
        public override void Init()
        {
            base.Init();
            //옵션창 띄울 때 시간 멈추자 싱글플레이어게임이니까
            Managers.Time.SetTimeScale(0);
            Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
            Bind<Button>(typeof(Buttons));
            Bind<Image>(typeof(Images));
            Bind<Slider>(typeof(Sliders));
        }
        
        void Update()
        {

            Get<Slider>((int)Sliders.Slider_MusicVolume).onValueChanged.AddListener(delegate { ValueCheck(); });
        }
        public override void ClosePopupUI()
        {
            //닫히기 전에 시간을 돌려놔야해
            Managers.Time.SetTimeScale(1);
            base.ClosePopupUI();
            
        }
        public void ValueCheck()
        {
            
        }
    }

}