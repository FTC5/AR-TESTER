using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.BasicUI
{
    internal class YPosLongButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private float moveDistance = .1f;

        private bool isButtonHeld = false;

        public event Action<float> onYPosChanged;

        void Update()
        {
            if (isButtonHeld)
            {
                onYPosChanged?.Invoke(moveDistance);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isButtonHeld = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isButtonHeld = false;
        }
    }
}
