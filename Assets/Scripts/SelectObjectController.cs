using System.Linq;
using UnityEngine;

public class SelectObjectController : MonoBehaviour
{
    private InteractableObjectManeger interactableObjectManeger;
    private string aRPlacementInteractableTag = "ARPlacementInteractable";
    private string aRObjectTag = "ARObject";
    // Start is called before the first frame update
    void Start()
    {
        interactableObjectManeger = GameObject.FindGameObjectWithTag(aRPlacementInteractableTag)?.GetComponent<InteractableObjectManeger>();
    }

    public void SelectObject()
    {
        interactableObjectManeger.ChangePlacementARObject(
            gameObject.transform.GetComponentsInChildren<Transform>().FirstOrDefault(child => child.tag == aRObjectTag));
    }
}
