using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    public class XRInteractionManagerBlockUI : XRInteractionManager
    {
        protected override void  ProcessInteractors(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                base.ProcessInteractors(updatePhase);
            }
            
        }
    }
}
