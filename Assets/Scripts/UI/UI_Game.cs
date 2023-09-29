using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Isometric.UI
{
    public class UI_Game : UI_Scene
    {

        public UI_Inventory inventory;
        public UI_Option option;
        public PlayerStat stat;
        public Image hp;
        public TextMeshProUGUI hp_text;
        private float currentFill;
        private float lerpSpeed = 2f;
        enum GameObjects
        {
            StatGroup,
            ActionButtons
        }
        enum Buttons
        {
            Btn_Action_1,
            Btn_Action_2,
            Btn_Action_3/*,

            Btn_Action_4,
            Btn_Action_1,
            Btn_Action_1,
            Btn_Action_1,
            Btn_Action_1,
            Btn_Action_1,
            Btn_Action_1,
*/
        }
        enum Images
        {
            Img_HP_Bar_BackGround,
            Img_HP_Bar_FillArea,
        }

        enum TextMeshProUGUIs
        { 
            Text_HP_Bar
        }
        private void Update()
        {
            Update_HPBar();
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                
                Managers.UI.ShowPopupUI<UI_Option>();
            }
        }

        private void Awake()
        {
            Init();
        }
        public override void Init()
        {
            base.Init();
            
            inventory = GetComponentInChildren<UI_Inventory>();
            //inventory.gameObject.SetActive(false);

            stat = FindObjectOfType<PlayerStat>().GetComponent<PlayerStat>();
            
            Bind<Button>(typeof(Buttons));
            Bind<GameObject>(typeof(GameObjects));
            Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
            Bind<Image>(typeof(Images));

            hp = GetImage((int)Images.Img_HP_Bar_FillArea);
            hp_text = GetText((int)TextMeshProUGUIs.Text_HP_Bar);
            BindEvent(GetButton((int)Buttons.Btn_Action_1).gameObject, PointerEventData => { });
            
        }

        private void Update_HPBar()
        {
            hp_text.text = stat.Hp + "/" + stat.MaxHp;
            currentFill = stat.Hp / (float)stat.MaxHp;
            
            if (currentFill != hp.fillAmount)
            {
                hp.fillAmount = Mathf.Lerp(hp.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
            }
        }
        
    }

}