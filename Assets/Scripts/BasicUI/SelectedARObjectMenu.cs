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
                arGestureInteractor.onSelectEntered.AddListener(SelectEntered);
                arGestureInteractor.onSelectExited.AddListener(SelectExited);
            }
        }

        void OnDisable()
        {
            if (arGestureInteractor != null)
            {
                arGestureInteractor.onSelectEntered.RemoveListener(SelectEntered);
                arGestureInteractor.onSelectExited.RemoveListener(SelectExited);
            }
        }

        void SelectEntered(XRBaseInteractable interactable)
        {
            arObjectIsSelected = interactable != null;
        }

        void SelectExited<XRBaseInteractable>(XRBaseInteractable interactable)
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
