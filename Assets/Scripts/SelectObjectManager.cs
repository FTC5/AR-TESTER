using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace Assets.Scripts
{
    [RequireComponent(typeof(ARGestureInteractor))]
    public class SelectObjectManager : MonoBehaviour, ISelectObjectManager
    {
        private ARGestureInteractor arGestureInteractor;
        private GameObject selectedObject;
        private string arObjectTag = "ARObject";

        void OnEnable()
        {
            arGestureInteractor = GetComponent<ARGestureInteractor>();
            if (arGestureInteractor == null)
            {
                this.enabled = false;
            }
            else
            {
                arGestureInteractor.selectEntered.AddListener(SelectEntered);
                arGestureInteractor.selectExited.AddListener(SelectExited);
            }
        }

        void OnDisable()
        {
            if (arGestureInteractor != null)
            {
                arGestureInteractor.selectEntered.RemoveListener(SelectEntered);
                arGestureInteractor.selectExited.RemoveListener(SelectExited);
            }
        }

        void SelectEntered(SelectEnterEventArgs eventArgs)
        {
            if (eventArgs.interactableObject != null && eventArgs.interactableObject.isSelected)
            {
                selectedObject = eventArgs.interactableObject.transform.gameObject;
            }
        }

        void SelectExited(SelectExitEventArgs eventArgs)
        {
            selectedObject = null;
        }

        public void DeleteSelectedObject()
        {
            if (selectedObject != null)
            {
                Destroy(selectedObject);
            }
        }

        public void ChangeColor(Color color)
        {
            if (selectedObject != null)
            {
                var arObject = selectedObject.transform.GetComponentsInChildren<Transform>().FirstOrDefault(child => child.tag == arObjectTag);

                if (arObject != null)
                {
                    arObject.GetComponent<MeshRenderer>().material.color = color;
                }
            }
        }

        public void SaveARObject()
        {
            if (selectedObject != null)
            {
                var arObject = selectedObject.transform.GetComponentsInChildren<Transform>().FirstOrDefault(child => child.tag == arObjectTag);

                if (arObject != null)
                {

                }
            }
        }

        public void AddToARObject(GameObject gameObject)
        {
            if (selectedObject != null)
            {
                var arObject = selectedObject.transform.GetComponentsInChildren<Transform>().FirstOrDefault(child => child.tag == arObjectTag);

                if (arObject != null)
                {

                }
            }
        }
    }
}
