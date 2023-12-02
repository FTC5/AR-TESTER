using UnityEngine;

public class PreferencesButtonClick : MonoBehaviour
{
    [SerializeField]
    private PreferencesSettings preferencesSettings;

    public void OpenCloseMenu()
    {
        if (preferencesSettings.IsActiveMenu)
        {
            closeMenu();
        }
        else
        {
            openMenu();
        }
        preferencesSettings.IsActiveMenu = !preferencesSettings.IsActiveMenu;
    }
    private void openMenu()
    {
        foreach (var item in preferencesSettings.PreferenceObjects)
        {
            item.SetActive();
        }
    }

    private void closeMenu()
    {
        foreach (var item in preferencesSettings.PreferenceObjects)
        {
            item.Hide();
        }
    }
}
