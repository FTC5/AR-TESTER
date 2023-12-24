using Assets.Scripts.BasicUI;
using LiteDB;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace Assets.Scripts
{
    [RequireComponent(typeof(ARGestureInteractor))]
    public class SelectObjectManager : MonoBehaviour, ISelectObjectManager
    {
        [SerializeField]
        private ColorPicker colorPicker;
        [SerializeField]
        private List<YPosLongButton> YPosBtn = new List<YPosLongButton>();
        [SerializeField]
        SelectObjectMenuContentController selectObjectMenu;
        private ARGestureInteractor arGestureInteractor;
        [SerializeField]
        private GameObject selectedObject;
        private Transform selectedARObject;
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

            if (colorPicker != null)
            {
                colorPicker.onColorChanged += ChangeColor;
            }

            foreach (var button in YPosBtn)
            {
                button.onYPosChanged += ChangeYPos;
            }
        }

        void OnDisable()
        {
            if (arGestureInteractor != null)
            {
                arGestureInteractor.selectEntered.RemoveListener(SelectEntered);
                arGestureInteractor.selectExited.RemoveListener(SelectExited);
            }

            if (colorPicker != null)
            {
                colorPicker.onColorChanged -= ChangeColor;
            }
        }

        void SelectEntered(SelectEnterEventArgs eventArgs)
        {
            if (eventArgs.interactableObject != null && eventArgs.interactableObject.isSelected)
            {
                selectedObject = eventArgs.interactableObject.transform.gameObject;
                selectedARObject = selectedObject.transform.GetComponentsInChildren<Transform>().FirstOrDefault(child => child.tag == arObjectTag);

                if (selectedARObject == null)
                    Logger.Instance.LogInfo("Selected AR Object " + selectedARObject.name);
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
                if (selectedARObject != null)
                {
                    selectedARObject.GetComponent<MeshRenderer>().material.color = color;
                }
            }
        }

        public void ChangeYPos(float moveDistance)
        {
            Vector3 newPosition = selectedObject.transform.position;
            newPosition.y += moveDistance;
            selectedObject.transform.position = newPosition;
        }

        public void SaveARObject()
        {
            var arObject = selectedObject.transform.GetComponentsInChildren<Transform>().FirstOrDefault(child => child.tag == arObjectTag);

            if (arObject != null)
            {
                if (arObject != null)
                {
                    var arGameObjects = new ARGameObjectSaveData(arObject).GetAll();
                    using (var db = new LiteDatabase(@$"{Application.persistentDataPath}/ARObject.db"))
                    {
                        var ARGameObjects = db.GetCollection<ARGameObjectSaveData>("ARGameObjects");
                        ARGameObjects.Insert(arGameObjects);
                    }

                    selectObjectMenu.AddContent(arObject.gameObject);
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
