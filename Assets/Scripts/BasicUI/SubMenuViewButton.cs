using UnityEngine;

public class SubMenuViewButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject SubMenu;

    public void OpenCloseSubMenu()
    {
        if (SubMenu != null)
        {
            SubMenu.SetActive(!SubMenu.activeSelf);
        }
    }

}
