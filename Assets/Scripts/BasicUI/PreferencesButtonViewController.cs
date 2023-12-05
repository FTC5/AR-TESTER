using UnityEngine;
using UnityEngine.UI;

public class PreferencesButtonViewController : MonoBehaviour
{
    [SerializeField]
    public float disappearTime = 3.0f;
    [SerializeField]
    private Image imageComponent;
    [SerializeField]
    private PreferencesSettings preferencesSettings;
    private bool isFadeIn = false;

    // Update is called once per frame
    void Update()
    {
        if (!preferencesSettings.IsActiveMenu && !isFadeIn)
        {
            fadeIn();
            isFadeIn = true;
        }
        else if (preferencesSettings.IsActiveMenu && isFadeIn)
        {
            imageComponent.CrossFadeAlpha(1f, 0.1f, false);
            isFadeIn = false;
        }
    }

    private void fadeIn()
    {
        imageComponent.CrossFadeAlpha(.0f, disappearTime, false);
    }
}
