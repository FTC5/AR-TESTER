using UnityEngine;
using UnityEngine.UI;

public class PreferencesButtonViewController : MonoBehaviour
{
    [SerializeField]
    public float disappearTime = 3.0f;
    [SerializeField]
    private Image imageComponent;
    [SerializeField]
    private Menu preferencesSettings;

    // Update is called once per frame
    void Update()
    {
        if (!preferencesSettings.IsActiveMenu && isNotFadeIn())
        {
            imageComponent.CrossFadeAlpha(.0f, disappearTime, false);
        }
    }

    private bool isNotFadeIn()
    {
        return imageComponent.canvasRenderer.GetAlpha() > .0f;
    }
}
