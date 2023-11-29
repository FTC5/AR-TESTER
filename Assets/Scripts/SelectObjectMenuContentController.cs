using System.Collections.Generic;
using UnityEngine;

public class SelectObjectMenuContentController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject selectObjectPrefab;
    [SerializeField]
    private List<GameObject> objectsPrefab;
    private float defaultScale = 56f;
    private string parentTag = "ParentObject";

    void Start()
    {
        addContent();
    }

    private void addContent()
    {
        
        foreach (var obj in objectsPrefab) 
        {
            var selectableObject = Instantiate(selectObjectPrefab, gameObject.transform);
            var parentComponent = getParentComponentForObjectsPrefab(selectableObject.transform);

            if(parentComponent != null)
            {
                GameObject newObject = Instantiate(obj, parentComponent);
                newObject.transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);
            }
        }
    }

    private Transform getParentComponentForObjectsPrefab(Transform selectableObject)
    {
        var childTransforms = selectableObject.GetComponentsInChildren<Transform>();
        
        if (childTransforms.Length > 0)
        {
            foreach (var childTransform in childTransforms)
            {
                if (childTransform.tag == parentTag)
                {
                    return childTransform;
                }
            }
        }

        return null;
    }
}
