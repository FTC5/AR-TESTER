using UnityEngine;

public class SubMenuViewButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject subMenu;

    [SerializeField]
    private MenuSubcomponentsViewer subcomponentsViewer;

    public void OpenCloseSubMenu()
    {
        if (subMenu != null)
        {
            if (!subMenu.activeInHierarchy && subcomponentsViewer != null)
            {
                subcomponentsViewer.HideElements(IMenuSubcomponentsViewer.ElementType.DefaultHidden);
            }

            subMenu.SetActive(!subMenu.activeInHierarchy);
        }
    }

}
