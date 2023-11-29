using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class InteractableObjectManeger : MonoBehaviour
{
    [SerializeField]
    private ARPlacementInteractable aRPlacementInteractable;
    [SerializeField]
    private GameObject placementPrefab;
    [SerializeField]
    private GameObject defaultPlacementARObject;
    private GameObject currentPlacementARObject;

    // Start is called before the first frame update
    void Start()
    {
        if (placementPrefab != null && aRPlacementInteractable != null)
        {
            aRPlacementInteractable.placementPrefab = placementPrefab;
            currentPlacementARObject = defaultPlacementARObject;
        }
    }

    public void ChangePlacementARObject(Transform placementARObject)
    {
        if(placementARObject != null)
        {
            GameObject newChildObject = Instantiate(placementARObject.gameObject, placementPrefab.transform);
            newChildObject.transform.localPosition = Vector3.zero;
            newChildObject.transform.localRotation = Quaternion.identity;
            newChildObject.transform.localScale = currentPlacementARObject.transform.localScale;
            newChildObject.SetActive(true);

            Destroy(currentPlacementARObject);
            currentPlacementARObject = newChildObject;
            aRPlacementInteractable.placementPrefab = placementPrefab;
        }
    }
}
