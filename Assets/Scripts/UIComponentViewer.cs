using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponentViewer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> defaultHiddenParts;
    // Start is called before the first frame update

    public void Hide()
    {
        hideDependentObjects();
        gameObject.SetActive(false);
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }

    private void hideDependentObjects()
    {
        foreach (var obj in defaultHiddenParts)
        {
            obj.SetActive(false);
        }
    }
}
