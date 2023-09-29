using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

namespace Isometric.UI
{
    public class UI_EventHandler : MonoBehaviour, IBeginDragHandler, IPointerClickHandler, IDragHandler, IEndDragHandler
    {

        public Action<PointerEventData> OnClickHandler = null;
        public Action<PointerEventData> OnBeginDragHandler = null;
        public Action<PointerEventData> OnDragHandler = null;
        public Action<PointerEventData> OnEndDragHandler = null;
        public Action<PointerEventData> OnPointerUpHandler = null;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(OnBeginDragHandler != null)
            {
                OnBeginDragHandler.Invoke(eventData);
            }
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (OnDragHandler != null)
            {
                OnDragHandler.Invoke(eventData);
            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if(OnEndDragHandler != null)
            {
                OnEndDragHandler.Invoke(eventData);
            }
        }

        

        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClickHandler != null)
            {
                OnClickHandler.Invoke(eventData);
            }
        }
        // Start is called before the first frame update


    }

}