using UnityEngine;

[RequireComponent(typeof(IMenuSubcomponentsViewer))]

public class Menu : MonoBehaviour
{
    IMenuSubcomponentsViewer subcomponentsViewer;
    public bool IsActiveMenu { get; set; }
    // Start is called before the first frame update

    void Start()
    {
        subcomponentsViewer = GetComponent<IMenuSubcomponentsViewer>();
    }

    public void OpenCloseMenu()
    {
        if (IsActiveMenu)
        {
            closeMenu();
        }
        else
        {
            openMenu();
        }
    }

    protected void openMenu()
    {
        subcomponentsViewer.ActivateElements();
        IsActiveMenu = true;
    }

    protected void closeMenu()
    {
        subcomponentsViewer.HideElements();
        IsActiveMenu = false;
    }
}

