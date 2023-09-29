using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;
namespace Isometric.UI
{
    
    public class UI_Loading : UI_Scene
    {

        [SerializeField] private ScreenFader screenFader;
        [SerializeField] private float delay = 1f;

        private void Awake()
        {
            screenFader = GetComponent<ScreenFader>();
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            screenFader.FadeOn();
        }
        public void FadeAndLoad()
        {
            StartCoroutine("FadeAndLoad_Coroutine");
        }
        private IEnumerator FadeAndLoad_Coroutine()
        {
            yield return new WaitForSeconds(delay);
            //json 파일들을 다 만들었을 경우에만 다음 씬으로 갈거야
            screenFader.FadeOff();
            //현재 로그인 씬 필요없으니까 메인메뉴로 바로 가버리자.
            Managers.Scene.LoadScene(Enums.Scene.SceneMainMenu);
            yield return new WaitForSeconds(screenFader.FadeOffDuration);
            
            Destroy(gameObject);
            
        }
    }
}