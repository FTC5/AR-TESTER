using UnityEngine;

public class SelectObjectButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject ObjectsPanel;
    private bool isOpenObjectsPanel;

    public void OpenMenu()
    {
        ObjectsPanel.SetActive(!isOpenObjectsPanel);
        isOpenObjectsPanel = !isOpenObjectsPanel;
    }

}
