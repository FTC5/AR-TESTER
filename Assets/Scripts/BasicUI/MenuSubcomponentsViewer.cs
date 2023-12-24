using System.Collections.Generic;
using UnityEngine;
using static IMenuSubcomponentsViewer;

public class MenuSubcomponentsViewer : MonoBehaviour, IMenuSubcomponentsViewer
{
    [SerializeField]
    private List<GameObject> hiddenComponent;
    [SerializeField]
    private List<GameObject> activeComponent;

    public void HideElements(ElementType type = ElementType.All)
    {
        if (type == ElementType.All || type == ElementType.DefaultHidden)
        {
            setComponentsActive(hiddenComponent, false);
        }

        if (type == ElementType.All || type == ElementType.DefaultActive)
        {
            setComponentsActive(activeComponent, false);
        }
    }

    public void ActivateElements()
    {
        setComponentsActive(activeComponent);
    }

    private void setComponentsActive(List<GameObject> components, bool isActive = true)
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
