using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;
using UnityEngine.UI;
namespace Isometric
{

    public class MouseScript : SingletonDontDestroyMonobehavior<MouseScript>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public IMoveable MyMoveable { get; set; }

        private Image moveIcon;
        [SerializeField] private Vector3 offset;

        private void Start()
        {
            moveIcon = GetComponent<Image>();
        }
        // Update is
        // called once per frame
        void Update()
        {
            moveIcon.transform.position = Input.mousePosition + offset;
        }

        public void TakeMoveable(IMoveable moveable)
        {
            this.MyMoveable = moveable;
            moveIcon.sprite = moveable.MyIcon;
            moveIcon.color = Color.white;
        }
    }

}