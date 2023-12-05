using System.Collections.Generic;
using UnityEngine;

public class MenuSubcomponentsViewer : MonoBehaviour, IMenuSubcomponentsViewer
{
    [SerializeField]
    private List<GameObject> hiddenComponent;
    [SerializeField]
    private List<GameObject> activeComponent;
    // Start is called before the first frame update

    public void HideElements()
    {
        setComponentsActive(hiddenComponent);
        setComponentsActive(activeComponent);
    }

    public void ActivateElements()
    {
        setComponentsActive(activeComponent, true);
    }

    private void setComponentsActive(List<GameObject> components, bool isActive = false)
    {
        if (components == null)
        {
            return;
        }

        foreach (var obj in components)
        {
            obj.SetActive(isActive);
        }
    }
}
