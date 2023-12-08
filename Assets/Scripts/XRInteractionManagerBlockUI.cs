using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Logger.Instance.SendMessage("=_=");
                base.ProcessInteractors(updatePhase);
            }
            
        }
    }
}
