using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
namespace Isometric
{

    public class ActionButton : MonoBehaviour, IPointerClickHandler
    {
        public IUseable MyUseable { get; set; }
        [SerializeField] private Image icon;

        public Image Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
            }
        }
        public Button MyButton { get; private set; }
        void Start()
        {
            MyButton = GetComponent<Button>();
            MyButton.onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            Debug.Log("언제 이게 실행되는걸까?");
            if (MyUseable != null)
            {
                MyUseable.Use();
            }
            else
            {
                MyUseable.Use();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {

        }
    }

}