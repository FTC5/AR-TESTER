using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace Assets.Scripts.BasicUI
{

    public class SelectedARObjectMenu : Menu
    {
        [SerializeField]
        private Menu preferencesMenu;
        [SerializeField]
        private ARGestureInteractor arGestureInteractor;
        private bool arObjectIsSelected;

        void OnEnable()
        {
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
            arObjectIsSelected = eventArgs.interactableObject != null && eventArgs.interactableObject.isSelected;
        }

        void SelectExited(SelectExitEventArgs eventArgs)
        {
            arObjectIsSelected = false;
        }

        void Update()
        {
            if (arObjectIsSelected)
            {
                if (!IsActiveMenu && !isPreferencesMenuActive())
                {
                    openMenu();
                }
                else if (IsActiveMenu && isPreferencesMenuActive())
                {
                    closeMenu();
                }
            }
            else if (IsActiveMenu && !arObjectIsSelected)
            {
                closeMenu();
            }
        }

        private bool isPreferencesMenuActive()
        {
            return preferencesMenu == null ? false : preferencesMenu.IsActiveMenu;
        }

    }
}
