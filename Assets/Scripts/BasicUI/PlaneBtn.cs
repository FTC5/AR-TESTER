using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Assets.Scripts.BasicUI
{
    public class PlaneBtn : MonoBehaviour
    {
        [SerializeField]
        private ARPlaneManager arPlaneManager;

        public void TogglePalne()
        {
            if(arPlaneManager.isActiveAndEnabled)
            {
                arPlaneManager.SetTrackablesActive(false);
                arPlaneManager.enabled = false;
            }
            else
            {
                arPlaneManager.enabled = true;
                arPlaneManager.SetTrackablesActive(true);
            }
        }
    }
}
