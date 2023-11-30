using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class RaycastDebuger : MonoBehaviour
{
    [SerializeField]
    private Text textObject;
    [SerializeField]
    private ARRaycastManager arRaycasterManager;
    private StringBuilder debugStr = new StringBuilder();
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                var touchPos = touch.position;

                if (EventSystem.current.IsPointerOverGameObject())
                {
                    debugStr.AppendLine("Raycast hit a UI element");
                }

                if (arRaycasterManager.Raycast(touchPos, hits))
                {
                    foreach (var hit in hits)
                    {
                        debugStr.AppendLine("trackable: " + hit.trackable.name);
                        debugStr.AppendLine("Hit Distance: " + hit.distance);
                    }
                }

                textObject.text = debugStr.ToString();

                debugStr.Clear();

            }
        }

    }
}
