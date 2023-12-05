using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace Assets.Scripts
{
    [RequireComponent(typeof(ARGestureInteractor))]
    public class SelectObjectManager : MonoBehaviour, ISelectObjectManager
    {
        public bool IsSelectARObject { get; set; }

        private ARGestureInteractor arGestureInteractor;
        private GameObject selectedObject;
        private string arObjectTag = "ARObject";

        void Start()
        {
            arGestureInteractor = GetComponent<ARGestureInteractor>();
            arGestureInteractor.onSelectEntered.AddListener((interactable) =>
            {
                IsSelectARObject = interactable != null;
                if (IsSelectARObject)
                {
                    selectedObject = interactable.gameObject;
                }

            });

            arGestureInteractor.onSelectExited.AddListener((interactable) =>
            {
                selectedObject = null;
                IsSelectARObject = false;
            });
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
